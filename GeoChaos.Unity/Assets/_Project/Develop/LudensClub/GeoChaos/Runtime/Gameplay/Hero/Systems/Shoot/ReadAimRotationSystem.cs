using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Shoot;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Shoot.Aim;
using LudensClub.GeoChaos.Runtime.Gameplay.Input;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Shoot
{
  public class ReadAimRotationSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsWorld _input;
    private readonly EcsEntities _aimings;
    private readonly EcsEntities _inputs;
    private readonly HeroConfig _config;

    public ReadAimRotationSystem(GameWorldWrapper gameWorldWrapper, InputWorldWrapper inputWorldWrapper, IConfigProvider configProvider)
    {
      _game = gameWorldWrapper.World;
      _input = inputWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _aimings = _game
        .Filter<Aiming>()
        .Collect();

      _inputs = _input
        .Filter<AimRotation>()
        .Inc<Expired>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity input in _inputs)
      foreach (EcsEntity aiming in _aimings)
      {
        Vector2 delta = input.Get<AimRotation>().Delta;
        aiming.Replace((ref ShootDirection direction) =>
        {
          direction.Direction += delta * _config.AimRotationSpeed * Time.deltaTime;
          direction.Direction.Normalize();
        });
      }
    }
  }
}