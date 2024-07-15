using System;
using System.Linq;
using LudensClub.GeoChaos.Runtime.Utils;
using Spine.Unity;
using TriInspector;
using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Spine
{
  public abstract partial class MonoSpineAnimator<TParameterEnum, TAnimationEnum> : MonoBehaviour, IInjectable,
    IInitializable, ITickable
    where TParameterEnum : Enum where TAnimationEnum : Enum
  {
    public SkeletonAnimation Skeleton;
    [LabelText("Animator Data")]
    public SpineAnimatorAsset<TParameterEnum, TAnimationEnum> SharedAnimatorData;

    [SerializeField]
    [ReadOnly]
    [InlineProperty]
    [HideLabel]
    protected SpineAnimator<TAnimationEnum> _animator;

    protected bool _needCheck;

    protected SpineAnimatorAsset<TParameterEnum, TAnimationEnum> _asset;
    protected int _sharedAssetId;
    protected bool _delayInitialize;
    protected bool _started;
    private TickableManager _ticker;

    public bool Injected { get; set; }

    [Inject]
    public void Construct(InitializableManager initializer, TickableManager ticker)
    {
      if (!_started)
        initializer.Add(this);

      _ticker = ticker;
      _ticker.Add(this);
      CreateAnimator();

      Injected = true;
    }

    public void Initialize()
    {
      if (Skeleton.state == null)
      {
        _delayInitialize = true;
        return;
      }

      _animator.StartAnimate();
      _delayInitialize = false;
    }

#if UNITY_EDITOR
    protected virtual void Start()
    {
      _started = true;
      if (!this.EnsureInjection())
        Initialize();
    }
#endif

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

    public virtual void SetVariable<TVariable>(TParameterEnum id, TVariable value)
    {
      SpineParameter<TParameterEnum> parameter = _asset.Parameters.Find(x => x.Id.Equals(id));
      if (parameter == null)
        throw new ArgumentException($"Parameter {id} is not found");

      var variableValue = parameter.Variable.GetValue<TVariable>();
      if (!variableValue.Equals(value))
      {
        parameter.Variable.SetValue(value);
        _needCheck = true;
      }
    }

    private void CreateAnimator()
    {
      _animator?.Dispose();

      _sharedAssetId = SharedAnimatorData.GetInstanceID();
      _asset = Instantiate(SharedAnimatorData);
      _animator = new SpineAnimator<TAnimationEnum>(Skeleton, _asset.Layers);
      foreach (SpineAnimatorLayer<TAnimationEnum> layer in _animator.Layers)
        layer.OnTransitionPerformed += ResetTriggers;
      
      foreach (SpineTransition<TParameterEnum, TAnimationEnum> transition in _asset.Transitions)
      {
        _animator.AddTransition(transition);
        foreach (ISpineCondition condition in transition.Conditions)
        {
          var id = condition.GetParameterId<TParameterEnum>();
          condition.Variable = _asset.Parameters.Find(x => x.Id.Equals(id)).Variable;
        }
      }
      
#if UNITY_EDITOR
      _showParameters.Clear();
      _showParameters.AddRange(_asset.Parameters.Select(x => new VariableTuple(x.Id, x.Variable)));
      
      _parameters.Clear();
      foreach (VariableTuple tuple in _showParameters)
        _parameters.Add(tuple.Id, tuple.Variable.GetValue());
#endif

      if (_started)
        Initialize();
    }

    private void ResetTriggers(SpineAnimationTransition<TAnimationEnum> transition)
    {
      foreach (ISpineCondition condition in transition.Data.Conditions)
      {
        var id = condition.GetParameterId<TParameterEnum>();
        SpineParameter<TParameterEnum> parameter = _asset.Parameters.Find(x => x.Id.Equals(id));
        if(parameter.IsTrigger)
          parameter.Variable.SetValue(false);
      }

#if UNITY_EDITOR
      _dirty = true;
#endif
    }
  }
}