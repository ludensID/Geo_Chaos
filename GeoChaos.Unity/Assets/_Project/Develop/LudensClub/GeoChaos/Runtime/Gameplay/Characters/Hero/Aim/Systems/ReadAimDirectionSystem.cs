﻿using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Shoot;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Input;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Aim
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
        .Filter<HeroTag>()
        .Inc<Aiming>()
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
          aiming.Change((ref ShootDirection shootDirection) => shootDirection.Direction = direction);
        }
      }
    }
  }
}