using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Damage;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Shroom.Attack.Reload
{
  public class ReloadShroomSystem : IEcsRunSystem
  {
    private readonly ITimerFactory _timers;
    private readonly EcsWorld _game;
    private readonly ShroomConfig _config;
    private readonly EcsEntities _attackingShrooms;

    public ReloadShroomSystem(GameWorldWrapper gameWorldWrapper, ITimerFactory timers, IConfigProvider configProvider)
    {
      _timers = timers;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<ShroomConfig>();

      _attackingShrooms = _game
        .Filter<ShroomTag>()
        .Inc<OnAttackStarted>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity shroom in _attackingShrooms)
      {
        shroom
          .Add<Reloading>()
          .Add((ref ReloadingTimer timer) => timer.TimeLeft = _timers.Create(_config.ReloadingTime));
      }
    }
  }
}