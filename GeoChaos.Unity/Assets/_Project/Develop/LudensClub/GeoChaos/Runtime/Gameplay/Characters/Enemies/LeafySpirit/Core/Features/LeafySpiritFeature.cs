﻿using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Attack;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Bide;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Correction;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Detection;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Leap;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Move;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Rise;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Wait;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit
{
  public class LeafySpiritFeature : EcsFeature
  {
    public LeafySpiritFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<LeafySpiritDetectionFeature>());
      Add(systems.Create<LeafySpiritWaitFeature>());
      Add(systems.Create<LeafySpiritLeapFeature>());
      Add(systems.Create<LeafySpiritRisingFeature>());
      Add(systems.Create<LeafySpiritCorrectionFeature>());
      Add(systems.Create<LeafySpiritMovingFeature>());
      Add(systems.Create<LeafySpiritAttackFeature>());
      Add(systems.Create<LeafySpiritBidingFeature>());
    }
  }
}