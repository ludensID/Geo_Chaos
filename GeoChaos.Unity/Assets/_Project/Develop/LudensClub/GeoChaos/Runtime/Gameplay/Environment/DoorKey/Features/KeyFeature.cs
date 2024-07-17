using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.DoorKey
{
  public class KeyFeature : EcsFeature
  {
    public KeyFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<TakeKeyByHeroSystem>());
    }
  }
}