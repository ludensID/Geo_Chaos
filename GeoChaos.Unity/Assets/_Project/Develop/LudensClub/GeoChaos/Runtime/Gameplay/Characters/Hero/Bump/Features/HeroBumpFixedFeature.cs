using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Bump
{
  public class HeroBumpFixedFeature : EcsFeature
  {
    public HeroBumpFixedFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<ResetToZeroVelocityWhenHeroBumpingSystem>());
    }    
  }
}