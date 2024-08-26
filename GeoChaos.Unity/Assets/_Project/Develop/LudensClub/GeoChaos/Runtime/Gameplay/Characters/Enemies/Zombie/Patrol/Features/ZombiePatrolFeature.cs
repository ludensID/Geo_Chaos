using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Patrol;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Patrol
{
  public class ZombiePatrolFeature : PatrolToRandomPointFeature<ZombieTag>
  {
    public ZombiePatrolFeature(IEcsSystemFactory systems) : base(systems)
    {
    }
  }
}