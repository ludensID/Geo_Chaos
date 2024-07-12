using System;
using System.Collections.Generic;
using Spine;
using Spine.Unity;
using TriInspector;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Spine
{
  [Serializable]
  public class SpineAnimatorLayer<TAnimationEnum> : IDisposable where TAnimationEnum : Enum
  {
    [HideInInspector]
    public readonly SkeletonAnimation Skeleton;

    [HideInInspector]
    public readonly List<SpineAnimationState<TAnimationEnum>> States = new List<SpineAnimationState<TAnimationEnum>>();

    public int Id;

    [NonSerialized]
    public SpineAnimationState<TAnimationEnum> Start;

    [NonSerialized]
    public SpineAnimationState<TAnimationEnum> Current;

    [NonSerialized]
    public SpineAnimationTransition<TAnimationEnum> NextTransition;

    [NonSerialized]
    public bool Hold;

    [ShowInInspector]
    [LabelText(nameof(Current))]
    public TAnimationEnum CurrentAnimation => Current != null ? Current.Animation.Name : default(TAnimationEnum);

    [ShowInInspector]
    [LabelText("Next")]
    public TAnimationEnum NextAnimation => Hold && NextTransition.Destination != null
      ? NextTransition.Destination.Animation.Name
      : default(TAnimationEnum);

    public event Action<SpineAnimationTransition<TAnimationEnum>> OnTransitionPerformed;

    public SpineAnimatorLayer(int id, SkeletonAnimation skeleton)
    {
      Id = id;
      Skeleton = skeleton;
    }

    public void ChangeAnimation(SpineAnimationState<TAnimationEnum> to)
    {
      Current = to;
      if (CheckTransition())
        return;
      // UnityEngine.Debug.Log($"{_enumName} Change to {Current.Animation.Name}");
      if (Current.Animation.Asset)
      {
        TrackEntry track = Skeleton.state.SetAnimation(Id, Current.Animation.Asset, Current.Animation.IsLoop);
        track.TimeScale = Current.Animation.Speed;
      }
      else
      {
        TrackEntry track = Skeleton.state.SetEmptyAnimation(Id, 0);
        track.TimeScale = 1;
      }
    }

    public void ChangeAnimation(SpineAnimationTransition<TAnimationEnum> transition)
    {
      OnTransitionPerformed?.Invoke(transition);
      ChangeAnimation(transition.Destination);
    }

    public bool CheckTransition()
    {
      ClearNext();
      SpineAnimationTransition<TAnimationEnum> transition = Current?.FindFirstCompletedCondition();
      // UnityEngine.Debug.Log($"It has {Current?.Transitions?.Count(x => x.Execute()) ?? 0} transitions");
      if (transition == null)
        return false;

      if (transition.Data.IsHold)
      {
        // UnityEngine.Debug.Log($"{_enumName} Transition now {Current.Animation.Name}");
        // UnityEngine.Debug.Log($"{_enumName} Transition hold from {transition.Destination.Animation.Name}");
        DelayAnimation(transition);
        return false;
      }

      // UnityEngine.Debug.Log($"{_enumName} Transition change {transition.Destination.Animation.Name}");
      ChangeAnimation(transition);
      return true;
    }

    public void ClearNext()
    {
      if (Hold)
      {
        Skeleton.state.Complete -= OnAnimationCompleted;
        Hold = false;
        NextTransition = null;
      }
    }

    public void DelayAnimation(SpineAnimationTransition<TAnimationEnum> transition)
    {
      // UnityEngine.Debug.Log($"{_enumName} Delay from {Skeleton.state.Tracks.Items[0].Animation.Name}");
      // UnityEngine.Debug.Log($"{_enumName} Delay to {to.Animation.Name}");
      Hold = true;
      NextTransition = transition;
      Skeleton.state.Complete += OnAnimationCompleted;
    }

    public void OnAnimationCompleted(TrackEntry trackEntry)
    {
      if (trackEntry.Animation != Current.Animation.Asset.Animation || trackEntry.TrackIndex != Id) return;

      // UnityEngine.Debug.Log($"{_enumName} Complete {trackEntry.Animation.Name}");
      // UnityEngine.Debug.Log($"{_enumName} Transit to {Next.Animation.Name}");
      ChangeAnimation(NextTransition);
    }

    public void Dispose()
    {
      if (Skeleton.state != null)
        ClearNext();
    }
  }
}