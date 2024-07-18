using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.Door
{
  public class DoorFeature : EcsFeature
  {
    public DoorFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<DetectHeroNearDoorSystem>());
      
      Add(systems.Create<FindMatchedKeySystem>());
      Add(systems.Create<OpenDoorSystem>());
      Add(systems.Create<SendBlankDoorInteractionSystem>());
      
      Add(systems.Create<OpenDoorFromOutsideSystem>());

      Add(systems.Create<OpenDoorViewSystem>());
    }
  }
}