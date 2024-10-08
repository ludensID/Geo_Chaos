﻿using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Move;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.View;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero
{
  public class SetHeroBodyDirectionSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _bodyDirections;

    public SetHeroBodyDirectionSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _bodyDirections = _game
        .Filter<HeroTag>()
        .Inc<BodyDirection>()
        .Inc<ViewDirection>()
        .Exc<BodyFreezing>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity bodyDir in _bodyDirections)
      {
        float direction = CalculateBodyDirection(bodyDir);
        bodyDir.Change((ref BodyDirection body) => body.Direction = direction);
      }
    }

    private static float CalculateBodyDirection(EcsEntity entity)
    {
      ref MovementVector vector = ref entity.Get<MovementVector>();
      float viewDirX = entity.Get<ViewDirection>().Direction.x;
      return (entity.Has<FreeRotating>() || (vector.Direction.x * vector.Speed.x).ApproximatelyEqual(0, 0.001f)) && viewDirX != 0
        ? viewDirX
        : vector.Direction.x;
    }
  }
}