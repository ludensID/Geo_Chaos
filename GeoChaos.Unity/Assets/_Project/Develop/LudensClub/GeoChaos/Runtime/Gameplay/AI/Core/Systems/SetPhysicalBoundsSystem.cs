using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
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
        .Filter<PatrolBounds>()
        .Inc<PhysicalBoundsRef>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity bound in _bounds)
      {
        Vector2 bounds = bound.Get<PhysicalBoundsRef>().GetBounds();
        bound.Change((ref PatrolBounds patrolBounds) => patrolBounds.Bounds = bounds);
      }
    }
  }
}