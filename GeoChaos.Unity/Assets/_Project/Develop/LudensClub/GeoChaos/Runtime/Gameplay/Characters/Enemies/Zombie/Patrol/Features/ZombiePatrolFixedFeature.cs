using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Patrol;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Patrol
{
  public class ZombiePatrolFixedFeature : PatrolToRandomPointFixedFeature<ZombieTag>
  {
    public ZombiePatrolFixedFeature(IEcsSystemFactory systems) : base(systems)
    {
    }
  }
}