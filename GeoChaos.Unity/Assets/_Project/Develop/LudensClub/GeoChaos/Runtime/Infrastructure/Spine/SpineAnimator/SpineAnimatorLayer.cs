using System;
using System.Collections.Generic;
using Spine;
using Spine.Unity;
using TriInspector;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Spine
{
  [Serializable]
  public class SpineAnimatorLayer : IDisposable
  {
    public readonly List<SpineAnimationState> States = new List<SpineAnimationState>();
    public readonly SkeletonAnimation Skeleton;

    public int Id;

    [NonSerialized]
    public SpineAnimationState Start;

    [NonSerialized]
    public SpineAnimationState Current;

    [NonSerialized]
    public SpineAnimationTransition NextTransition;

    [NonSerialized]
    public bool Hold;

    [ShowInInspector]
    [LabelText(nameof(Current))]
    public string CurrentAnimation => Current != null ? Current.Animation.Name : "None";

    [ShowInInspector]
    [LabelText("Next")]
    public string NextAnimation => Hold && NextTransition.Destination != null
      ? NextTransition.Destination.Animation.Name
      : "None";

    public event Action<SpineAnimationTransition> OnTransitionPerformed;

    public SpineAnimatorLayer(int id, SkeletonAnimation skeleton)
    {
      Id = id;
      Skeleton = skeleton;
    }

    public void ChangeAnimation(SpineAnimationState to)
    {
      Current = to;
      if (CheckTransition())
        return;
      // UnityEngine.Debug.Log($"{Skeleton.AnimationName} Change to {CurrentAnimation}");
      if (Current.Animation.Name != "")
      {
        Animation animation = Skeleton.Skeleton.Data.FindAnimation(Current.Animation.Name);
        TrackEntry track = Skeleton.state.SetAnimation(Id, animation, Current.Animation.IsLoop);
        track.TimeScale = Current.Animation.Speed;
      }
      else
      {
        TrackEntry track = Skeleton.state.SetEmptyAnimation(Id, 0);
        track.TimeScale = 1;
      }
    }

    public void ChangeAnimation(SpineAnimationTransition transition)
    {
      OnTransitionPerformed?.Invoke(transition);
      ChangeAnimation(transition.Destination);
    }

    public bool CheckTransition()
    {
      ClearNext();
      SpineAnimationTransition transition = Current?.FindFirstCompletedCondition();
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

    public void DelayAnimation(SpineAnimationTransition transition)
    {
      // UnityEngine.Debug.Log($"{_enumName} Delay from {Skeleton.state.Tracks.Items[0].Animation.Name}");
      // UnityEngine.Debug.Log($"{_enumName} Delay to {to.Animation.Name}");
      Hold = true;
      NextTransition = transition;
      Skeleton.state.Complete += OnAnimationCompleted;
    }

    public void OnAnimationCompleted(TrackEntry trackEntry)
    {
      if (trackEntry.Animation.Name != Current.Animation.Name || trackEntry.TrackIndex != Id) return;

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