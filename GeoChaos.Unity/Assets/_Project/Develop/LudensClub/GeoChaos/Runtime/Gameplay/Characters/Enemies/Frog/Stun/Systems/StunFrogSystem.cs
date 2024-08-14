using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Stun
{
  public class StunFrogSystem : IEcsRunSystem
  {
    private readonly ITimerFactory _timers;
    private readonly EcsWorld _game;
    private readonly EcsEntities _stunnedFrogs;
    private readonly FrogConfig _config;

    public StunFrogSystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider, ITimerFactory timers)
    {
      _timers = timers;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<FrogConfig>();
      
      _stunnedFrogs = _game
        .Filter<FrogTag>()
        .Inc<StunCommand>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity frog in _stunnedFrogs)
      {
        frog
          .Del<StunCommand>()
          .Add<Stunned>()
          .Add((ref StunTimer timer) => timer.TimeLeft = _timers.Create(_config.StunTime));
      }
    }
  }
}