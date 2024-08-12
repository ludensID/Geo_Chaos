using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Bite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Attack.Bite
{
  public class FrogBiteSystem : IEcsRunSystem
  {
    private readonly ITimerFactory _timers;
    private readonly EcsWorld _game;
    private readonly EcsEntities _bitingFrogs;
    private readonly FrogConfig _config;

    public FrogBiteSystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider, ITimerFactory timers)
    {
      _timers = timers;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<FrogConfig>();

      _bitingFrogs = _game
        .Filter<FrogTag>()
        .Inc<BiteCommand>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity frog in _bitingFrogs)
      {
        frog
          .Del<BiteCommand>()
          .Add<OnBiteStarted>()
          .Add<Biting>()
          .Add((ref BiteTimer timer) => timer.TimeLeft = _timers.Create(_config.BiteTime));
      }
    }
  }
}