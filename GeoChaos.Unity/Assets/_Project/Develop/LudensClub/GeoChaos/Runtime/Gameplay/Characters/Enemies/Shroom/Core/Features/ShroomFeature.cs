﻿using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Shroom.Attack;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Shroom.Detection;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Shroom.GasShooting;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Shroom.Patrol;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Shroom.Wait;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Shroom.Watch.Features;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Shroom
{
  public class ShroomFeature : EcsFeature
  {
    public ShroomFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<ShroomDetectionFeature>());
        
      Add(systems.Create<ShroomWaitFeature>());
      Add(systems.Create<ShroomPatrolFeature>());
      Add(systems.Create<ShroomAttackFeature>());
      Add(systems.Create<ShroomWatchFeature>());
      
      Add(systems.Create<ShroomGasShootingCycleFeature>());
    }
  }
}