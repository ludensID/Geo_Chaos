using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemies.Lama.Patrol
{
  public class LamaPatrolFeature : EcsFeature
  {
    public LamaPatrolFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<DeleteLamaOnPatrolledSystem>());
      Add(systems.Create<PatrolLamaSystem>());
      Add(systems.Create<DeleteLamaPatrolCommandSystem>());
      Add(systems.Create<StopPatrollingSystem>());
    }
  }
}