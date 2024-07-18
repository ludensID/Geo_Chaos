using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.View
{
  public class ViewFeature : EcsFeature
  {
    public ViewFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<SetViewRotationSystem>());
      Add(systems.Create<SetActiveViewSystem>());
      
      Add(systems.Create<ShowNothingHappensWindowSystem>());
      Add(systems.Create<Delete<NothingHappensMessage, MessageWorldWrapper>>());
    }
  }
}