using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Attack.AttackMove;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Shroom.GasShooting;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Shroom.Attack.AttackMove
{
  public class IncreaseGasShotCounterSystem : IEcsRunSystem
  {
    private readonly ITimerFactory _timers;
    private readonly EcsWorld _game;
    private readonly EcsEntities _shootingShrooms;
    private readonly ShroomConfig _config;

    public IncreaseGasShotCounterSystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider, ITimerFactory timers)
    {
      _timers = timers;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<ShroomConfig>();

      _shootingShrooms = _game
        .Filter<ShroomTag>()
        .Inc<AttackMoving>()
        .Inc<OnGasShot>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity shroom in _shootingShrooms)
      {
        ref GasShotCounter counter = ref shroom.Get<GasShotCounter>();
        counter.Count++;

        if (counter.Count >= _config.ShotNumber)
        {
          float time = _config.BrakingDistance / shroom.Get<AttackMoveSpeed>().Speed;
          shroom
            .Add<StopGasShootingCycleCommand>()
            .Add((ref BrakeTimer timer) => timer.TimeLeft = _timers.Create(time));
        }
      }
    }
  }
}