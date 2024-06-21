using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Enemies.Lama;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.AI
{
  public class SetPhysicalBoundsSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _bounds;

    public SetPhysicalBoundsSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _bounds = _game
        .Filter<PhysicalBounds>()
        .Inc<BoundsRef>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity bound in _bounds)
      {
        Vector2 bounds = bound.Get<BoundsRef>().GetBounds();
        ref PhysicalBounds physicalBounds = ref bound.Get<PhysicalBounds>();
        if (physicalBounds.Bounds != bounds)
        {
          physicalBounds.Bounds = bounds;
          bound.Add<CalculateBoundsCommand>();
        }
      }
    }
  }
}