using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Aim;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Input;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Shoot
{
  public class ReadAimPositionSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsWorld _input;
    private readonly EcsEntities _aimings;
    private readonly EcsEntities _inputs;
    private readonly HeroConfig _config;

    public ReadAimPositionSystem(GameWorldWrapper gameWorldWrapper, InputWorldWrapper inputWorldWrapper)
    {
      _game = gameWorldWrapper.World;
      _input = inputWorldWrapper.World;

      _aimings = _game
        .Filter<HeroTag>()
        .Inc<Aiming>()
        .Collect();

      _inputs = _input
        .Filter<AimPosition>()
        .Inc<AimRotation>()
        .Inc<Expired>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity input in _inputs)
      foreach (EcsEntity aiming in _aimings)
      {
        ref ShootPosition shootPosition = ref aiming.Get<ShootPosition>();
        if (aiming.Has<OnAimStarted>())
        {
          shootPosition.Origin = input.Get<AimPosition>().Position;
          shootPosition.Position = shootPosition.Origin;
        }

        Vector2 delta = input.Get<AimRotation>().Rotation;
        if (delta != Vector2.zero)
        {
          shootPosition.Position += delta;

          Vector2 shootVector = shootPosition.Position - shootPosition.Origin;
          shootVector = Vector2.ClampMagnitude(shootVector, 100);
          shootPosition.Position = shootPosition.Origin + shootVector;
          
          Vector2 direction = shootVector.normalized;
          if (direction != Vector2.zero)
            aiming.Change((ref ShootDirection shootDirection) => shootDirection.Direction = direction);
        }
      }
    }
  }
}