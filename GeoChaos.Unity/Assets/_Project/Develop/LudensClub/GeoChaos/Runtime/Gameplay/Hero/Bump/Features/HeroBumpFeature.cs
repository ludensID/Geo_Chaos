using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Bump
{
  public class HeroBumpFeature : EcsFeature
  {
    public HeroBumpFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<HeroBumpSystem>());
      Add(systems.Create<StopFreezeBodySystem>());
      Add(systems.Create<StopHeroBumpSystem>());
    } 
  }
}