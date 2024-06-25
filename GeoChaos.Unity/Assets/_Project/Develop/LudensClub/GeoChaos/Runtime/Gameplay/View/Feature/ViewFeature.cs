using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.View
{
  public class ViewFeature : EcsFeature
  {
    public ViewFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<SetViewRotationSystem>());
    }
  }
}