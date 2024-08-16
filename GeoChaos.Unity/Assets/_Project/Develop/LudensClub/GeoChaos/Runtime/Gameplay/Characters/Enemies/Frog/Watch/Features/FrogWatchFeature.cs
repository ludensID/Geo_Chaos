using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Watch
{
  public class FrogWatchFeature : EcsFeature
  {
    public FrogWatchFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<FrogWatchWhenWasAimedSystem>());
      Add(systems.Create<StartFrogWatchingTimerSystem>());
      
      Add(systems.Create<DeleteExpiredFrogWatchingTimerSystem>());
      Add(systems.Create<DeleteWatchingTimerForAimedFrogSystem>());
    }
  }
}