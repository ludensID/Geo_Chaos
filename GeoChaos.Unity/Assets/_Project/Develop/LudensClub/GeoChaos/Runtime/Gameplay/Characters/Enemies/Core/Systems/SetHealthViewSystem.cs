using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Characteristics;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies
{
  public class SetHealthViewSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _enemies;

    public SetHealthViewSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _enemies = _game
        .Filter<EnemyTag>()
        .Inc<HealthRef>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity enemy in _enemies)
      {
        enemy.Get<HealthRef>().View.SetText(enemy.Get<CurrentHealth>().Health.ToString("###0"));
      }
    }
  }
}