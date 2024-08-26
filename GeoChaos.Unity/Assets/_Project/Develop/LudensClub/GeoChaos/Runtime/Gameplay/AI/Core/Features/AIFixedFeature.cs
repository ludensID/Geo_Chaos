using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Patrol;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Patrol;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.AI
{
  public class AIFixedFeature : EcsFeature
  {
    public AIFixedFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<AIPatrolFixedFeature>());
    }
  }
}