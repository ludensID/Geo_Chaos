﻿using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics
{
  public class ViewFixedFeature : EcsFeature
  {
    public ViewFixedFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<SetViewVelocitySystem>());
      Add(systems.Create<SetViewGravitySystem>());
    }
  }
}