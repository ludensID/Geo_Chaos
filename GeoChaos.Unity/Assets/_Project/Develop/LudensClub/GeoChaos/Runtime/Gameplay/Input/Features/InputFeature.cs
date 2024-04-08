using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Input
{
  public class InputFeature : EcsFeature
  {
    public InputFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<InitializeInputDelaySystem>());

      Add(systems.Create<DestroyExpiredEntitiesSystem>());
      Add(systems.Create<UpdateExpireTimeSystem>());
      Add(systems.Create<ReadInputSystem>());
      Add(systems.Create<MarkExpireUpEntitiesSystem>());
      Add(systems.Create<MarkExpiredEntitySystem>());
    }
  }
}