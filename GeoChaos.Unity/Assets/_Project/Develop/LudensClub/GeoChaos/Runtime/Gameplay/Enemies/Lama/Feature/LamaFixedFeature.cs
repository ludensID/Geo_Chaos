﻿using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemies.Lama
{
  public class LamaFixedFeature : EcsFeature
  {
    public LamaFixedFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<CheckLamaForPatrollingTimerExpiredSystem>());
      Add(systems.Create<CheckLamaForLookingTimerExpiredSystem>());
    }
  }
}