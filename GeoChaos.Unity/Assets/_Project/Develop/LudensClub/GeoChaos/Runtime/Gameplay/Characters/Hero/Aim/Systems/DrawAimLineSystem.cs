using System.Collections.Generic;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Shoot;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Aim
{
  public class DrawAimLineSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _aimings;
    private readonly EcsEntities _finishedAimings;
    private readonly HeroConfig _config;
    private readonly ContactFilter2D _filter;
    private readonly List<RaycastHit2D> _hits;

    public DrawAimLineSystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider)
    {
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _aimings = _game
        .Filter<HeroTag>()
        .Inc<ShootLineRef>()
        .Inc<Aiming>()
        .Collect();

      _finishedAimings = _game
        .Filter<OnAimFinished>()
        .Collect();

      _hits = new List<RaycastHit2D>(1);
      _filter = new ContactFilter2D
      {
        useTriggers = false,
        useLayerMask = true,
        layerMask = _config.ShootMask
      };
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity aiming in _aimings)
      {
        Vector3 origin = aiming.Get<ViewRef>().View.transform.position;
        var direction = (Vector3)aiming.Get<ShootDirection>().Direction; 
        
        Vector3 target = origin + direction * 100;
        _hits.Clear();
        int count = Physics2D.Raycast(origin, direction, _filter, _hits);
        if (count > 0)
          target = _hits[0].point;
        
        ref ShootLineRef lineRef = ref aiming.Get<ShootLineRef>();
        lineRef.Line.positionCount = 2;
        lineRef.Line.SetPositions(new[] { origin, target });
      }

      foreach (EcsEntity aiming in _finishedAimings)
      {
        aiming.Change((ref ShootLineRef lineRef) => lineRef.Line.positionCount = 0);
      }
    }
  }
}