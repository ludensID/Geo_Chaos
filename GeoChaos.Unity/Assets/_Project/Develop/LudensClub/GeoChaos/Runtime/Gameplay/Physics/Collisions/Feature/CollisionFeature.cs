﻿using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Collisions
{
  public class CollisionFeature : EcsFeature
  {
    public CollisionFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<DeleteCollisionSystem>());
      Add(systems.Create<PackCollisionsSystem>());
    }
  }
}