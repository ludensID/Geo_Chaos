using System;
using System.Collections.Generic;
using System.Linq;
using Spine.Unity;
using TriInspector;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Spine
{
  [Serializable]
  public class SpineAnimator : IDisposable
  {
    private readonly SkeletonAnimation _skeleton;

    private readonly List<SpineAnimationState> _states = new List<SpineAnimationState>();

    [SerializeField]
    [ListDrawerSettings(Draggable = false, HideAddButton = true, HideRemoveButton = true)]
    private List<SpineAnimatorLayer> _layers = new List<SpineAnimatorLayer>();

    public List<SpineAnimatorLayer> Layers => _layers;

    public SpineAnimator(SkeletonAnimation skeleton, List<SpineLayer> layers)
    {
      _skeleton = skeleton;
      for (int i = 0; i < layers.Count; i++)
      {
        _layers.Add(new SpineAnimatorLayer(i, skeleton));
        SpineAnimationState[] states = layers[i].Animations
          .Select(x => new SpineAnimationState(x)).ToArray();
        _states.AddRange(states);
        AddAnimationsToLayer(i, layers[i].StartAnimation, states);
      }
    }

    public SpineAnimator(SkeletonAnimation skeleton,
      ConfigurableSpineAnimation[] anims,
      int layerCount = 1,
      string start = "")
    {
      for (int i = 0; i < layerCount; i++)
        _layers.Add(new SpineAnimatorLayer(i, skeleton));

      foreach (var anim in anims)
        _states.Add(new SpineAnimationState(anim));

      if (layerCount == 1)
        AddAnimationsToLayer(0, start, _states.ToArray());
    }

    public SpineAnimator AddAnimationsToLayer(int index,
      string start = "",
      params SpineAnimationState[] anims)
    {
      SpineAnimatorLayer layer = Layers[index];
      layer.Start ??= GetState(start);
      layer.States.AddRange(anims);

      return this;
    }

    public SpineAnimator AddAnimationsToLayer(int index,
      string start = "",
      params ConfigurableSpineAnimation[] anims)
    {
      return AddAnimationsToLayer(index, start, _states.Where(x => anims.Contains(x.Animation)).ToArray());
    }

    public SpineAnimator AddAnimationsToLayer(int index, string start = "", params string[] names)
    {
      return AddAnimationsToLayer(index, start, names.Select(GetState).ToArray());
    }

    public SpineAnimator AddTransition(SpineTransition transition)
    {
      foreach (string id in transition.Origins)
      {
        GetState(id)
          .Transitions
          .Add(
            new SpineAnimationTransition
            {
              Data = transition,
              Destination = GetState(transition.Destination)
            });
      }

      return this;
    }

    public SpineAnimationState GetState(string animationName)
    {
      return _states.Single(x => x.Animation.Name.Equals(animationName));
    }

    public void StartAnimate()
    {
      foreach (SpineAnimatorLayer layer in _layers)
      {
        if (layer.Start == null)
          throw new ArgumentNullException(nameof(layer.Start),
            $"Animator layer by index {layer.Id} has not a start animation");
      }

      foreach (SpineAnimatorLayer layer in _layers)
        layer.ChangeAnimation(layer.Start);
    }

    public void CheckTransition()
    {
      foreach (SpineAnimatorLayer layer in _layers)
        layer.CheckTransition();
    }

    public void Dispose()
    {
      foreach (SpineAnimatorLayer layer in Layers)
        layer.Dispose();
    }
  }
}