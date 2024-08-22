using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Patrol
{
  public class ZombiePatrolFeature : EcsFeature
  {
    public ZombiePatrolFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<ZombiePatrollingSystem>());

      Add(systems.Create<DeleteZombiePatrolFinishedEventSystem>());
      Add(systems.Create<FinishZombiePatrollingSystem>());

      Add(systems.Create<StopZombiePatrollingSystem>());
    }
  }
}