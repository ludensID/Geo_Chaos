using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class CheckForHeroJumpStopSystem : IEcsRunSystem
  {
    private readonly EcsWorld _world;
    private readonly EcsFilter _heroes;

    public CheckForHeroJumpStopSystem(GameWorldWrapper gameWorldWrapper)
    {
      _world = gameWorldWrapper.World;

      _heroes = _world.Filter<HeroTag>()
        .Inc<JumpAvailable>()
        .Inc<StopJumpCommand>()
        .Inc<Velocity>()
        .End();
    }

    public void Run(EcsSystems systems)
    {
      foreach (var hero in _heroes)
      {
        ref var velocity = ref _world.Get<Velocity>(hero);

        if (velocity.Value.y <= 0)
          _world.Del<StopJumpCommand>(hero);
      }
    }
  }
}