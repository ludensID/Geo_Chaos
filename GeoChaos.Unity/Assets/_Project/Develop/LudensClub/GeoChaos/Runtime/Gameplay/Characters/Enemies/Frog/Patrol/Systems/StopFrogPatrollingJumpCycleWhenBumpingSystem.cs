using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Patrol;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Bump;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.JumpCycle;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Patrol
{
  public class StopFrogPatrollingJumpCycleWhenBumpingSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _patrollingFrogs;

    public StopFrogPatrollingJumpCycleWhenBumpingSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _patrollingFrogs = _game
        .Filter<FrogTag>()
        .Inc<Patrolling>()
        .Inc<Bumping>()
        .Exc<DelayedPatrol>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity frog in _patrollingFrogs)
      {
        frog
          .Add<DelayedPatrol>()
          .Add<PauseJumpCycleCommand>();
      }
    }
  }
}