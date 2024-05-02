using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Move;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Features
{
  public class HeroPhysicsFeature : EcsFeature
  {
    public HeroPhysicsFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<IncreaseDragSystem>());
      Add(systems.Create<IncreaseControlSystem>());
      Add(systems.Create<DragHookVelocitySystem>());
      Add(systems.Create<UpdateControlSystem>());
      Add(systems.Create<DecreaseHookVelocitySystem>());
      Add(systems.Create<CheckForControlDelaySystem>());
      Add(systems.Create<DeleteControlSystem>());
      Add(systems.Create<CheckForHeroReachRingSystem>());
      Add(systems.Create<CheckForHookTimerSystem>());
      // Add(systems.Create<DirectHookVelocitySystem>());
    }
  }
}