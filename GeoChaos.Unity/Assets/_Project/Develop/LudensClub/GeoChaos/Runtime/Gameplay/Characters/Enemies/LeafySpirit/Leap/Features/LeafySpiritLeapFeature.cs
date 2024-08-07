﻿using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Leap
{
  public class LeafySpiritLeapFeature : EcsFeature
  {
    public LeafySpiritLeapFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<PrecastLeafySpiritLeapSystem>());
      
      Add(systems.Create<StartLeafySpiritLeapSystem>());
      Add(systems.Create<CalculateAimedLeafySpiritLeapPositionSystem>());
      Add(systems.Create<CalculateCalmLeafySpiritLeafPositionSystem>());
      Add(systems.Create<LeafySpiritLeapSystem>());
      
      Add(systems.Create<DeleteLeafySpiritLeapFinishedEventSystem>());
      Add(systems.Create<FinishLeafySpiritLeapSystem>());
      Add(systems.Create<StopLeafySpiritLeapSystem>());
    } 
  }
}