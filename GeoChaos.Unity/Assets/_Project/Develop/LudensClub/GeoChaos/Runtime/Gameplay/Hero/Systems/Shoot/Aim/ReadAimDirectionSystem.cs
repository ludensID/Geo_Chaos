using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Shoot;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Shoot.Aim;
using LudensClub.GeoChaos.Runtime.Gameplay.Input;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Shoot.Aim
{
  public class ReadAimDirectionSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsWorld _input;
    private readonly EcsEntities _aimings;
    private readonly EcsEntities _inputs;

    public ReadAimDirectionSystem(GameWorldWrapper gameWorldWrapper, InputWorldWrapper inputWorldWrapper)
    {
      _game = gameWorldWrapper.World;
      _input = inputWorldWrapper.World;

      _aimings = _game
        .Filter<Aiming>()
        .Collect();

      _inputs = _input
        .Filter<AimDirection>()
        .Inc<Expired>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity input in _inputs)
      foreach (EcsEntity aiming in _aimings)
      {
        Vector2 direction = input.Get<AimDirection>().Direction;
        if (direction != Vector2.zero)
        {
          aiming.Replace((ref ShootDirection shootDirection) => shootDirection.Direction = direction);
        }
      }
    }
  }
}