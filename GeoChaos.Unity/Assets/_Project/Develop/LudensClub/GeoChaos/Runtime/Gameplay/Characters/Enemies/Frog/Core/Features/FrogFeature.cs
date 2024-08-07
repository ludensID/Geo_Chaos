﻿using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Jump;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.JumpWait;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Patrol;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Wait;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog
{
  public class FrogFeature : EcsFeature
  {
    public FrogFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<InitializeFrogSystem>());
        
      Add(systems.Create<FrogWaitFeature>());
      Add(systems.Create<FrogPatrolFeature>());
      
      Add(systems.Create<FrogJumpFeature>());
      Add(systems.Create<FrogJumpWaitFeature>());
    }
  }
}