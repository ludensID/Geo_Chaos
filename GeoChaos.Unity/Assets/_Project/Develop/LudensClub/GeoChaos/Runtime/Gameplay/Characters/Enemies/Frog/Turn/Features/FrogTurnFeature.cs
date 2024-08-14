using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Turn
{
  public class FrogTurnFeature : EcsFeature
  {
    public FrogTurnFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<StartFrogTurningTimerSystem>());
      Add(systems.Create<StopFrogTurningTimerSystem>());
      Add(systems.Create<CheckForFrogTurningTimerExpiredSystem>());      
        
      Add(systems.Create<DelayFrogTurnWhenBumpingSystem>());
      Add(systems.Create<TurnFrogWhenFinishBumpingSystem>());
      
      Add(systems.Create<FinishTurnFrogSystem>());
    }
  }
}