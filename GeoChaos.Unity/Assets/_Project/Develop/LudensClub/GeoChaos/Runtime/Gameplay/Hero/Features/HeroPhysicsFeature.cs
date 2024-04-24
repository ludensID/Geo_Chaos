using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Hook;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Features
{
  public class HeroPhysicsFeature : EcsFeature
  {
    public HeroPhysicsFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<ReadHeroViewVelocitySystem>());
      
      Add(systems.Create<CheckForHeroReachRingSystem>());
      Add(systems.Create<CheckForHookTimerSystem>());
      
      Add(systems.Create<CalculateHeroVelocitySystem>());
      
      Add(systems.Create<SetHeroViewVelocitySystem>());
    }
  }
}