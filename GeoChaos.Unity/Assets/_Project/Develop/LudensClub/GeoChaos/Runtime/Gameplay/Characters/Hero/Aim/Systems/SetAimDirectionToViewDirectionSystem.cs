﻿using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Shoot;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.View;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Aim
{
  public class SetAimDirectionToViewDirectionSystem : IEcsRunSystem
  {
    private readonly IShootService _shootSvc;
    private readonly EcsWorld _game;
    private readonly EcsEntities _startedAims;

    public SetAimDirectionToViewDirectionSystem(GameWorldWrapper gameWorldWrapper, IShootService shootSvc)
    {
      _shootSvc = shootSvc;
      _game = gameWorldWrapper.World;

      _startedAims = _game
        .Filter<HeroTag>()
        .Inc<OnAimStarted>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity aim in _startedAims)
      {
        Vector2 direction =
          _shootSvc.CalculateShootDirection(aim.Get<ViewDirection>().Direction, aim.Get<BodyDirection>().Direction);
        aim.Change((ref ShootDirection shootDirection) => shootDirection.Direction = direction);
      }
    }
  }
}