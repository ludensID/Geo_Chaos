using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Ring;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Hook
{
  public class SelectReachedRingsSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _heroes;
    private readonly EcsEntities _selectedRings;
    private readonly PhysicsConfig _physics;

    public SelectReachedRingsSystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider)
    {
      _game = gameWorldWrapper.World;
      _physics = configProvider.Get<PhysicsConfig>();
      
      _heroes = _game
        .Filter<HeroTag>()
        .Inc<ViewRef>()
        .Collect();

      _selectedRings = _game
        .Filter<RingTag>()
        .Inc<ViewRef>()
        .Inc<Selected>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity hero in _heroes)
      foreach (EcsEntity ring in _selectedRings)
      {
        Vector3 heroPosition = hero.Get<ViewRef>().View.transform.position;
        Vector3 ringPosition = ring.Get<ViewRef>().View.transform.position;
        
        Vector3 vector = ringPosition - heroPosition;
        RaycastHit2D centerRaycast = Physics2D.Raycast(heroPosition, vector.normalized, vector.magnitude,
          _physics.GroundMask);
        RaycastHit2D topRaycast = Physics2D.Raycast(heroPosition + Vector3.up, vector.normalized,
          vector.magnitude, _physics.GroundMask);
        
        if (centerRaycast.collider != null || topRaycast.collider != null)
          ring.Del<Selected>();
      }
    }
  }
}