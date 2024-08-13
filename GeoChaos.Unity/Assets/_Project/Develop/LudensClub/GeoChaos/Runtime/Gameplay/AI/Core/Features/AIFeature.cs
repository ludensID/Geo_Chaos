using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Patrol;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.AI
{
  public class AIFeature : EcsFeature
  {
    public AIFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<CreateBehaviourTreeSystem>());
      Add(systems.Create<SetStartPositionSystem>());
      
      Add(systems.Create<AIPatrolFeature>());
      
      Add(systems.Create<RunBehaviourTreeSystem>());
    } 
  }
}