using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Attack.Bite
{
  public class FrogBiteFeature : EcsFeature
  {
    public FrogBiteFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<DeleteFrogBiteStartedEventSystem>());
      Add(systems.Create<FrogBiteSystem>());
      
      Add(systems.Create<DeleteFrogBiteFinishedEventSystem>());
      Add(systems.Create<CheckForFrogBiteTimerExpiredSystem>());
      
      Add(systems.Create<DeleteFrogBiteStoppedEventSystem>());
      Add(systems.Create<StopFrogBiteSystem>());

      Add(systems.Create<EnableFrogBiteColliderSystem>());
    }
  }
}