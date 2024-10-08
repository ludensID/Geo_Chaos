﻿using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Move
{
  public class HeroMovingFeature : EcsFeature
  {
    public HeroMovingFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<DeleteSystem<MoveHeroCommand>>());
      Add(systems.Create<ReadMovementSystem>());
      Add(systems.Create<CalculateHeroMovementVectorSystem>());
      Add(systems.Create<IgnoreMovementSystem>());
    }
  }
}