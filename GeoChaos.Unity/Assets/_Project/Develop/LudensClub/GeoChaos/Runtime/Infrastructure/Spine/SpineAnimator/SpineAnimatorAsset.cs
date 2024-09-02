using System.Collections.Generic;
using Spine.Unity;
using TriInspector;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Spine
{
  [CreateAssetMenu(menuName = CAC.Names.SPINE_ANIMATOR_MENU, fileName = CAC.Names.SPINE_ANIMATOR_FILE)]
  public class SpineAnimatorAsset : ScriptableObject, IHasSkeletonDataAsset, IHasSpineParameters
  {
    private readonly Dictionary<int, SpineParameter> _cachedParameters = new Dictionary<int, SpineParameter>();

    [SerializeField]
    protected SkeletonDataAsset _skeletonDataAsset;

    public SkeletonDataAsset SkeletonDataAsset => _skeletonDataAsset;

    [SerializeField]
    [HideReferencePicker]
    [ListDrawerSettings(AlwaysExpanded = true)]
    [ValidateInput("ValidateParameters")]
    private List<SpineParameter> _parameters = new List<SpineParameter>();

    [ListDrawerSettings(ShowElementLabels = true)]
    public List<SpineLayer> Layers;

    [ListDrawerSettings(ShowElementLabels = true)]
    [ValidateInput("ValidateTransitions")]
    public List<SpineTransition> Transitions;

    public List<SpineParameter> Parameters => _parameters;

    private void OnEnable()
    {
      foreach (SpineParameter parameter in Parameters)
      {
        _cachedParameters[parameter.Name.GetHashCode()] = parameter;
      }
    }

    public bool TryGetParameterByHash(int parameterHash, out SpineParameter parameter)
    {
      return _cachedParameters.TryGetValue(parameterHash, out parameter);
    }
    
    public SpineParameter GetParameterByHash(int parameterHash)
    {
      return _cachedParameters[parameterHash];
    }

#if UNITY_EDITOR
    private TriValidationResult ValidateParameters()
    {
      foreach (SpineParameter parameter in Parameters)
      {
        if (Parameters.Exists(x => parameter != x && parameter.Name == x.Name))
          return TriValidationResult.Error($"Parameter {parameter.Name} already exists");
      }

      return TriValidationResult.Valid;
    }

    private TriValidationResult ValidateTransitions()
    {
      foreach (SpineTransition transition in Transitions)
      {
        foreach (SpineCondition condition in transition.Conditions)
        {
          if(!Parameters.Exists(x => x.Name == condition.Parameter))
            return TriValidationResult.Error($"Parameter {condition.Parameter} do not exists");
        }
      }

      return TriValidationResult.Valid;
    }
#endif
  }
}