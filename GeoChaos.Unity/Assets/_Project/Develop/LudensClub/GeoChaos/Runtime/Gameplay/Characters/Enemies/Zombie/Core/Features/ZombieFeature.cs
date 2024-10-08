﻿using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.ArmsAttack;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Attack;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Detection;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Patrol;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Wait;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Watch;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie
{
  public class ZombieFeature : EcsFeature
  {
    public ZombieFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<ZombieDetectionFeature>());
      
      Add(systems.Create<ZombieWaitFeature>());
      Add(systems.Create<ZombiePatrolFeature>());
      Add(systems.Create<ZombieAttackFeature>());
      Add(systems.Create<ZombieWatchFeature>());
      
      Add(systems.Create<ZombieArmsAttackFeature>());
    }
  }
}