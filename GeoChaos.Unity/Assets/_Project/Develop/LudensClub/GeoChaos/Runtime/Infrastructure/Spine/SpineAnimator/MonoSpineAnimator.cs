using System;
using System.Linq;
using Spine.Unity;
using TriInspector;
using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Spine
{
  [AddComponentMenu(ACC.Names.MONO_SPINE_ANIMATOR)]
  [RequireComponent(typeof(MonoInjector))]
  public partial class MonoSpineAnimator : MonoBehaviour, IInitializable, ITickable
  {
    public SkeletonAnimation Skeleton;
    [LabelText("Animator Data")]
    public SpineAnimatorAsset SharedAnimatorData;

    [SerializeField]
    [ReadOnly]
    [InlineProperty]
    [HideLabel]
    protected SpineAnimator _animator;

    protected bool _needCheck;

    protected SpineAnimatorAsset _asset;
    protected int _sharedAssetId;
    protected bool _delayInitialize;
    
    private bool _initialized;
    private TickableManager _ticker;
    private IInitializingPhase _phase;

    [Inject]
    public void Construct(IInitializingPhase phase, TickableManager ticker)
    {
      _phase = phase;
      _phase.Add(this);

      _ticker = ticker;
      _ticker.Add(this);
      CreateAnimator();
    }

    public void Initialize()
    {
      _initialized = true;
      
      if (Skeleton.state == null)
      {
        _delayInitialize = true;
        return;
      }

      _animator.StartAnimate();
      _delayInitialize = false;
    }

    protected virtual void Start()
    {
      if(_phase.WasInitialized && !_initialized)
        Initialize();
    }

    public void Tick()
    {
      if (_delayInitialize)
      {
        Initialize();
        return;
      }

      if (_sharedAssetId != SharedAnimatorData.GetInstanceID())
        CreateAnimator();

#if UNITY_EDITOR
      CheckUserParameters();
#endif

      if (_needCheck)
      {
        _animator.CheckTransition();
        _needCheck = false;
      }

#if UNITY_EDITOR
      SyncUserParameters();
#endif
    }

    private void OnDestroy()
    {
      _ticker.Remove(this);
    }

    public virtual void SetVariable<TVariable>(string parameterName, TVariable value)
    {
      SpineParameter parameter = _asset.Parameters.Find(x => x.Name == parameterName);
      if (parameter == null)
        throw new ArgumentException($"Parameter {parameterName} is not found");

      var variableValue = parameter.Variable.GetValue<TVariable>();
      if (!variableValue.Equals(value))
      {
        parameter.Variable.SetValue(value);
        _needCheck = true;
      }
    }

    public virtual void SetTrigger(string parameterName, bool value = true)
    {
      SetVariable(parameterName, value);
    }
    
    public virtual void SetVariable<TVariable>(int parameterHash, TVariable value)
    {
      if (!_asset.TryGetParameterByHash(parameterHash, out SpineParameter parameter))
        throw new ArgumentException($"Parameter with {parameterHash} hash is not found");

      var variableValue = parameter.Variable.GetValue<TVariable>();
      if (!variableValue.Equals(value))
      {
        parameter.Variable.SetValue(value);
        _needCheck = true;
      }
    }
    
    public virtual void SetTrigger(int parameterHash, bool value = true)
    {
      SetVariable(parameterHash, value);
    }

    private void CreateAnimator()
    {
      _animator?.Dispose();

      _sharedAssetId = SharedAnimatorData.GetInstanceID();
      _asset = Instantiate(SharedAnimatorData);
      _animator = new SpineAnimator(Skeleton, _asset.Layers);
      foreach (SpineAnimatorLayer layer in _animator.Layers)
        layer.OnTransitionPerformed += ResetTriggers;
      
      foreach (SpineTransition transition in _asset.Transitions)
      {
        _animator.AddTransition(transition);
        foreach (SpineCondition condition in transition.Conditions)
        {
          condition.Variable = _asset.GetParameterByHash(condition.Parameter.GetHashCode()).Variable;
        }
      }
      
#if UNITY_EDITOR
      _showParameters.Clear();
      _showParameters.AddRange(_asset.Parameters.Select(x => new VariableTuple(x.Name, x.Variable)));
      
      _parameters.Clear();
      foreach (VariableTuple tuple in _showParameters)
        _parameters.Add(tuple.ParameterName, tuple.Variable.GetValue());
#endif

      if (_initialized)
        Initialize();
    }

    private void ResetTriggers(SpineAnimationTransition transition)
    {
      foreach (SpineCondition condition in transition.Data.Conditions)
      {
        SpineParameter parameter = _asset.GetParameterByHash(condition.Parameter.GetHashCode());
        if(parameter.IsTrigger)
          parameter.Variable.SetValue(false);
      }

#if UNITY_EDITOR
      _dirty = true;
#endif
    }
  }
}