using System;
using System.Collections.Generic;
using Spine;
using Spine.Unity;
using TriInspector;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Spine
{
  [Serializable]
  public class SpineAnimatorLayer<TAnimationEnum> where TAnimationEnum : Enum
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
    public SpineAnimationState<TAnimationEnum> Next;

    [ShowInInspector]
    [LabelText(nameof(Current))]
    public TAnimationEnum CurrentAnimation => Current != null ? Current.Animation.Name : default(TAnimationEnum);

    [ShowInInspector]
    [LabelText(nameof(Next))]
    public TAnimationEnum NextAnimation => Next != null ? Next.Animation.Name : default(TAnimationEnum);

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
        Skeleton.state.SetAnimation(Id, Current.Animation.Asset, Current.Animation.IsLoop);
      else
        Skeleton.state.SetEmptyAnimation(Id, 0);
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
        DelayAnimation(transition.Destination);
        return false;
      }

      // UnityEngine.Debug.Log($"{_enumName} Transition change {transition.Destination.Animation.Name}");
      ChangeAnimation(transition.Destination);
      return true;
    }

    public void ClearNext()
    {
      if (Next != null)
      {
        Skeleton.state.Complete -= OnAnimationCompleted;
        Next = null;
      }
    }

    public void DelayAnimation(SpineAnimationState<TAnimationEnum> to)
    {
      // UnityEngine.Debug.Log($"{_enumName} Delay from {Skeleton.state.Tracks.Items[0].Animation.Name}");
      // UnityEngine.Debug.Log($"{_enumName} Delay to {to.Animation.Name}");
      Next = to;
      Skeleton.state.Complete += OnAnimationCompleted;
    }

    public void OnAnimationCompleted(TrackEntry trackEntry)
    {
      if (trackEntry.Animation != Current.Animation.Asset.Animation || trackEntry.TrackIndex != Id) return;

      // UnityEngine.Debug.Log($"{_enumName} Complete {trackEntry.Animation.Name}");
      // UnityEngine.Debug.Log($"{_enumName} Transit to {Next.Animation.Name}");
      ChangeAnimation(Next);
      Skeleton.state.Complete -= OnAnimationCompleted;
    }
  }
}