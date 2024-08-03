using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characteristics.Endurance
{
  public class EnduranceBoundingFeature : EcsFeature
  {
    public EnduranceBoundingFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<BoundCurrentEnduranceByMinSystem>());      
    }
  }
}