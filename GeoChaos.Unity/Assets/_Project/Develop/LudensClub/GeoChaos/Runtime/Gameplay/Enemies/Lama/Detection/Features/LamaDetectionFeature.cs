﻿using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemies.Lama.Detection
{
  public class LamaDetectionFeature : EcsFeature
  {
    public LamaDetectionFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<AimOnHeroSystem>());
      Add(systems.Create<CheckHeroInLamaViewSystem>());
    }
  }
}