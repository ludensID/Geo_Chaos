using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Attack.AttackMove;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Shroom.Attack.Reload;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Shroom.GasShooting;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Shroom.Attack.AttackMove
{
  public class StartShroomAttackMoveSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _movingShrooms;
    private readonly ShroomConfig _config;

    public StartShroomAttackMoveSystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider)
    {
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<ShroomConfig>();

      _movingShrooms = _game
        .Filter<ShroomTag>()
        .Inc<OnReloadingFinished>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity shroom in _movingShrooms)
      {
        shroom
          .Add<AttackMoveCommand>()
          .Add<StartGasShootingCycleCommand>()
          .Replace((ref GasShotCounter counter) => counter.Count = 0)
          .Replace((ref GasShootingCooldownTime time) => time.Time = _config.AttackShotCooldown);
      }
    }
  }
}