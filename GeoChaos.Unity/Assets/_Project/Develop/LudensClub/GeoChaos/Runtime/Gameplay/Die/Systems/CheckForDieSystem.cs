using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characteristics.Endurance;
using LudensClub.GeoChaos.Runtime.Gameplay.Characteristics.Health;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Damage;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Die
{
  public class CheckForDieSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _damageables;

    public CheckForDieSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _damageables = _game
        .Filter<Damageable>()
        .Exc<Immortal>()
        .Exc<Died>()
        .Collect();

    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity entity in _damageables)
      {
        if (entity.Has<CurrentHealth>() && entity.Get<CurrentHealth>().Health <= 0
          || entity.Has<CurrentEndurance>() && entity.Get<CurrentEndurance>().Endurance <= 0)
        {
          entity
            .Add<Died>()
            .Add<OnDied>();
        }
      }
    }
  }
}