using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Ring;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Hook
{
  public class SelectNearestRingSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _heroes;
    private readonly EcsEntities _selectedRings;

    public SelectNearestRingSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

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
      var minDistance = float.MaxValue;
      EcsEntity nearestRing = null;
      
      foreach (EcsEntity hero in _heroes)
      foreach (EcsEntity ring in _selectedRings)
      {
        Transform heroTransform = hero.Get<ViewRef>().View.transform;
        Transform ringTransform = ring.Get<ViewRef>().View.transform;
        float distance = Vector3.Distance(ringTransform.position,  heroTransform.position);
        if (distance < minDistance)
        {
          minDistance = distance;
          nearestRing = ring;
        }

        ring.Del<Selected>();
      }

      nearestRing?.Add<Selected>();
    }
  }
}