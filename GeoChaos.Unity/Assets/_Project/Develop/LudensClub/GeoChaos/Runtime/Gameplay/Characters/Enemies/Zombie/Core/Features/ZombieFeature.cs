using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Patrol;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Wait;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie
{
  public class ZombieFeature : EcsFeature
  {
    public ZombieFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<ZombieWaitFeature>());
      Add(systems.Create<ZombiePatrolFeature>());
    }
  }
}