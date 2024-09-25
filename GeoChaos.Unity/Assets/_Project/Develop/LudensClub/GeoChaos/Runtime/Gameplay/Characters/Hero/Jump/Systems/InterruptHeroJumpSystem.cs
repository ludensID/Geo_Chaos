using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Jump;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Jump
{
  public class InterruptHeroJumpSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _jumpings;

    public InterruptHeroJumpSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _jumpings = _game
        .Filter<HeroTag>()
        .Inc<Jumping>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity jumping in _jumpings
        .Check<MovementLayout>(x => x.Layer != MovementLayer.All))
      {
        jumping.Del<Jumping>();
      }
    }
  }
}