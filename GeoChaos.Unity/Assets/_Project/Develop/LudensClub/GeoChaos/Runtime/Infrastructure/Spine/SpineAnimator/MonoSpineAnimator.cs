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
    public SpineAnimatorAsset<TParameterEnum, TAnimationEnum> SharedAsset;

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

    public bool Injected { get; set; }

    [Inject]
    public void Construct(InitializableManager initializer, TickableManager ticker)
    {
      if (!_started)
        initializer.Add(this);
      ticker.Add(this);
      CreateAnimator();

      Injected = true;
    }

    private void CreateAnimator()
    {
      _sharedAssetId = SharedAsset.GetInstanceID();
      _asset = Instantiate(SharedAsset);
      _animator = new SpineAnimator<TAnimationEnum>(Skeleton, _asset.Layers);
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

    public void Initialize()
    {
      if (Skeleton.state == null)
      {
        _delayInitialize = true;
        return;
      }

      _animator.StartAnimate();
      Debug.Log("Initialize");
      _delayInitialize = false;
    }

    protected virtual void Start()
    {
#if UNITY_EDITOR
      _started = true;
      if (!this.EnsureInjection())
        Initialize();
#endif
    }

    public void Tick()
    {
      if (_delayInitialize)
      {
        Initialize();
        return;
      }

      if (_sharedAssetId != SharedAsset.GetInstanceID())
        CreateAnimator();

#if UNITY_EDITOR
      CheckParameters();
#endif

      if (_needCheck)
      {
        _animator.CheckTransition();
        ClearTriggers();
        _needCheck = false;
      }
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

    protected virtual void ClearTriggers()
    {
      foreach (SpineParameter<TParameterEnum> parameter in _asset.Parameters.Where(x => x.IsTrigger))
      {
        parameter.Variable.SetValue(false);
      }
    }
  }
}