﻿using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemies.Lama.Chase
{
  public class LamaChasingFeature : EcsFeature
  {
    public LamaChasingFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<ChaseHeroByLamaSystem>());
      Add(systems.Create<KeepLamaChasingSystem>());
      Add(systems.Create<StopChaseHeroByLamaSystem>());
    }
  }
}