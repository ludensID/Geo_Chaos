using System;
using System.Collections.Generic;
using System.Linq;
using Spine.Unity;
using TriInspector;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Spine
{
  [Serializable]
  public class SpineAnimator<TAnimationEnum> where TAnimationEnum : Enum
  {
    private readonly List<SpineAnimationState<TAnimationEnum>> _states = new List<SpineAnimationState<TAnimationEnum>>();
    
    [SerializeField]
    [ListDrawerSettings(Draggable = false, HideAddButton = true, HideRemoveButton = true)]
    private List<SpineAnimatorLayer<TAnimationEnum>> _layers = new List<SpineAnimatorLayer<TAnimationEnum>>();

    public SpineAnimator(SkeletonAnimation skeleton, List<SpineLayer<TAnimationEnum>> layers)
    {
      for (int i = 0; i < layers.Count; i++)
      {
        _layers.Add(new SpineAnimatorLayer<TAnimationEnum>(i, skeleton));
        SpineAnimationState<TAnimationEnum>[] states = layers[i].Animations
          .Select(x => new SpineAnimationState<TAnimationEnum>(x)).ToArray();
        _states.AddRange(states);
        AddAnimationsToLayer(i, layers[i].StartAnimation, states);
      }
    }

    public SpineAnimator(SkeletonAnimation skeleton,
      ConfigurableSpineAnimation<TAnimationEnum>[] anims,
      int layerCount = 1,
      TAnimationEnum start = default(TAnimationEnum))
    {
      for (int i = 0; i < layerCount; i++)
        _layers.Add(new SpineAnimatorLayer<TAnimationEnum>(i, skeleton));

      foreach (var anim in anims)
        _states.Add(new SpineAnimationState<TAnimationEnum>(anim));

      if (layerCount == 1)
        AddAnimationsToLayer(0, start, _states.ToArray());
    }

    public SpineAnimator<TAnimationEnum> AddAnimationsToLayer(int index,
      TAnimationEnum start = default(TAnimationEnum),
      params SpineAnimationState<TAnimationEnum>[] anims)
    {
      var layer = GetLayer(index);
      layer.Start ??= GetState(start);
      layer.States.AddRange(anims);

      return this;
    }

    public SpineAnimator<TAnimationEnum> AddAnimationsToLayer(int index,
      TAnimationEnum start = default(TAnimationEnum),
      params ConfigurableSpineAnimation<TAnimationEnum>[] anims)
    {
      return AddAnimationsToLayer(index, start, _states.Where(x => anims.Contains(x.Animation)).ToArray());
    }

    public SpineAnimator<TAnimationEnum> AddAnimationsToLayer(int index, TAnimationEnum start = default(TAnimationEnum), params TAnimationEnum[] names)
    {
      return AddAnimationsToLayer(index, start, names.Select(GetState).ToArray());
    }

    public SpineAnimator<TAnimationEnum> AddTransition(ISpineTransition<TAnimationEnum> transition)
    {
      foreach (TAnimationEnum id in transition.Origins)
      {
        GetState(id)
          .Transitions
          .Add(
            new SpineAnimationTransition<TAnimationEnum>
            {
              Data = transition,
              Destination = GetState(transition.Destination)
            });
      }

      return this;
    }

    public SpineAnimatorLayer<TAnimationEnum> GetLayer(int index) => _layers[index];

    public SpineAnimationState<TAnimationEnum> GetState(TAnimationEnum type) =>
      _states.Single(x => x.Animation.Name.Equals(type));

    public void StartAnimate()
    {
      foreach (SpineAnimatorLayer<TAnimationEnum> layer in _layers)
      {
        if (layer.Start == null)
          throw new ArgumentNullException(nameof(layer.Start),
            $"Animator layer by index {layer.Id} has not a start animation");
      }

      foreach (SpineAnimatorLayer<TAnimationEnum> layer in _layers)
        layer.ChangeAnimation(layer.Start);
    }

    public void CheckTransition()
    {
      foreach (SpineAnimatorLayer<TAnimationEnum> layer in _layers)
        layer.CheckTransition();
    }
  }
}