using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Environment.GasCloud;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Shroom.GasShooting
{
  public class ShootGasSystem : IEcsRunSystem
  {
    private readonly GasCloudPool _pool;
    private readonly ITimerFactory _timers;
    private readonly EcsWorld _game;
    private readonly EcsEntities _shootingShrooms;

    public ShootGasSystem(GameWorldWrapper gameWorldWrapper, GasCloudPool pool, ITimerFactory timers)
    {
      _pool = pool;
      _timers = timers;
      _game = gameWorldWrapper.World;

      _shootingShrooms = _game
        .Filter<ShroomTag>()
        .Inc<ShootingGas>()
        .Exc<GasShootingCooldown>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity shroom in _shootingShrooms)
      {
        Vector3 position = shroom.Get<GasCloudPointRef>().Point.position;
        GasCloudView view = _pool.Pop(position, Quaternion.identity);
        EcsEntity gasCloud = _game.CreateEntity();
        view.Converter.SetEntity(gasCloud);
        view.Converter.SendCreateMessage();
        gasCloud.Add((ref Owner owner) => owner.Entity = shroom.PackedEntity);

        shroom
          .Add<OnGasShot>()
          .Add((ref GasShootingCooldown cooldown) =>
            cooldown.TimeLeft = _timers.Create(shroom.Get<GasShootingCooldownTime>().Time));
      }
    }
  }
}