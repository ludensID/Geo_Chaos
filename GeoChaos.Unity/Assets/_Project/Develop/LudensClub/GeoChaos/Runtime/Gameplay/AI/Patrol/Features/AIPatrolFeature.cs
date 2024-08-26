using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.AI.Patrol
{
  public class AIPatrolFeature : EcsFeature
  {
    public AIPatrolFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<AddBoundsRefSystem>());
      Add(systems.Create<SetPhysicalBoundsSystem>());
    }
  }
}