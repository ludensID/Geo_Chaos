using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Bump
{
  public class StopHeroBumpSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _bumpedHeroes;

    public StopHeroBumpSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _bumpedHeroes = _game
        .Filter<HeroTag>()
        .Inc<BumpTimer>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity hero in _bumpedHeroes
        .Where<BumpTimer>(x => x.TimeLeft <= 0))
      {
        hero.Del<BumpTimer>();
        ref MovementLayout layout = ref hero.Get<MovementLayout>();
        if (layout.Owner == MovementType.Bump)
        {
          layout.Layer = MovementLayer.All;
          layout.Owner = MovementType.None;
        }
      }
    }
  }
}