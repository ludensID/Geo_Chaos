using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Chase;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.JumpCycle;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Chase
{
  public class StopFrogChasingSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _chasingFrogs;

    public StopFrogChasingSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _chasingFrogs = _game
        .Filter<FrogTag>()
        .Inc<StopChaseCommand>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity frog in _chasingFrogs)
      {
        frog
          .Del<StopChaseCommand>()
          .Has<Chasing>(false)
          .Add<StopJumpCycleCommand>();
      }
    }
  }
}