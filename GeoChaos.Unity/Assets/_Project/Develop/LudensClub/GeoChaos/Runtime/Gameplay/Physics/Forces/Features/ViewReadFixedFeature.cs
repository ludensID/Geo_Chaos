﻿using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.View;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces
{
  public class ViewReadFixedFeature : EcsFeature
  {
    public ViewReadFixedFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<ReadViewVelocitySystem>());
    }
  }
}