using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.AI
{
  public class AiFeature : EcsFeature
  {
    public AiFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<CreateBehaviourTreeSystem>());
      Add(systems.Create<SetStartPositionSystem>());
      
      Add(systems.Create<SetPhysicalBoundsSystem>());
      
      Add(systems.Create<RunBehaviourTreeSystem>());
    } 
  }
}