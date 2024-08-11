using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Move
{
  public class InterruptMovementSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _movings;

    public InterruptMovementSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _movings = _game
        .Filter<HeroTag>()
        .Inc<Moving>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity moving in _movings
        .Check<MovementLayout>(x => x.Layer != MovementLayer.All))
      {
        moving.Del<Moving>();
      }
    }
  }
}