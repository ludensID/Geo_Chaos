using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Patrol;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Wait;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Watch;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.JumpWait;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Turn;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Damage;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.AI.BehaviourTrees;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Patrol
{
  public class CheckForFrogPatrollingStrategy : IConditionStrategy
  {
    private readonly EcsWorld _game;
    public EcsEntity Entity { get; set; }

    public CheckForFrogPatrollingStrategy(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;
    }

    public bool Check()
    {
      return !Entity.Has<Attacking>() 
        && (!Entity.Has<Aimed>() && !Entity.Has<OnPatrolFinished>() && !Entity.Has<WaitingTimer>()
          && !Entity.Has<WatchingTimer>() && !Entity.Has<TurningTimer>()
          || Entity.Has<Patrolling>() && !Entity.Has<OnJumpWaitFinished>());
    }
  }
}