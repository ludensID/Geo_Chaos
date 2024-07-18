using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Characteristics
{
  public class CharacteristicBoundingFeature : EcsFeature
  {
    public CharacteristicBoundingFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<BoundCurrentHealthByMaxSystem>());
      Add(systems.Create<BoundCurrentHealthByMinSystem>());
    }
  }
}