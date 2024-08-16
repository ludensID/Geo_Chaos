using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Patrol;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Wait;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Watch;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.JumpWait;
using LudensClub.GeoChaos.Runtime.Gameplay.Damage;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.AI.BehaviourTrees;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Patrol
{
  public class CheckForFrogPatrollingStrategy : IConditionStrategy
  {
    public EcsEntity Entity { get; set; }

    public bool Check()
    {
      return !Entity.Has<Attacking>() 
        && (!Entity.Has<Aimed>() && !Entity.Has<OnPatrolFinished>() 
          && !Entity.Has<WaitingTimer>() && !Entity.Has<WatchingTimer>()
          || Entity.Has<Patrolling>() && !Entity.Has<OnJumpWaitFinished>());
    }
  }
}