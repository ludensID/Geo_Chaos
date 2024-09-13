using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Interaction
{
  public class InteractionFeature : EcsFeature
  {
    public InteractionFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<DetectEntityToInteractSystem>());
    }
  }
}