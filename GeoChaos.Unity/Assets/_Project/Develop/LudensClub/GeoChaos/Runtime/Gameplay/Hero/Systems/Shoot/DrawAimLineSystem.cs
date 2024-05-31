using System;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Shoot;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Shoot
{
  public class DrawAimLineSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _aimings;
    private readonly EcsEntities _finishedAimings;
    private readonly HeroConfig _config;

    public DrawAimLineSystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider)
    {
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _aimings = _game
        .Filter<ShootLineRef>()
        .Inc<Aiming>()
        .Collect();

      _finishedAimings = _game
        .Filter<OnAimFinished>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity aiming in _aimings)
      {
        Vector3 origin = aiming.Get<ViewRef>().View.transform.position;
        var direction = (Vector3)aiming.Get<ShootDirection>().Direction; 
        
        Vector3 target = origin + direction * 100;
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, Single.MaxValue, _config.ShootMask);
        if (hit.collider)
          target = hit.point;
        
        ref ShootLineRef lineRef = ref aiming.Get<ShootLineRef>();
        lineRef.Line.positionCount = 2;
        lineRef.Line.SetPositions(new[]
        {
          origin,
          target
        });
      }

      foreach (EcsEntity aiming in _finishedAimings)
      {
        aiming.Replace((ref ShootLineRef lineRef) => lineRef.Line.positionCount = 0);
      }
    }
  }
}