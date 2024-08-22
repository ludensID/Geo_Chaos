using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Attack;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Patrol;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Watch;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie
{
  public class ZombieFixedFeature : EcsFeature
  {
    public ZombieFixedFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<ZombiePatrolFixedFeature>());
      Add(systems.Create<ZombieAttackFixedFeature>());
      Add(systems.Create<ZombieWatchFixedFeature>());
    }
  }
}