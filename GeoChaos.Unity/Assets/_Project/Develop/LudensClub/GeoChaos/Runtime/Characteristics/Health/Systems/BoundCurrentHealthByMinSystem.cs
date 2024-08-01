using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Characteristics.Health
{
  public class BoundCurrentHealthByMinSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _healthEntities;

    public BoundCurrentHealthByMinSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _healthEntities = _game
        .Filter<CurrentHealth>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity entity in _healthEntities)
      {
        entity.Change((ref CurrentHealth health) => health.Health = MathUtils.Clamp(health.Health, 0));
      }
    }
  }
}