using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.AI.Patrol
{
  public class AIPatrolFixedFeature : EcsFeature
  {
    public AIPatrolFixedFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<KeepInBoundsSystem>());
      Add(systems.Create<CheckForEntitiesInBoundsSystem>());
    }
  }
}