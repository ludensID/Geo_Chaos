using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Shroom.Watch.Systems;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Shroom.Watch.Features
{
  public class ShroomWatchFeature : EcsFeature
  {
    public ShroomWatchFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<StartShroomWatchSystem>());
      Add(systems.Create<StopShroomWatchWhenTimerExpiredSystem>());
      Add(systems.Create<StopAimedShroomWatchSystem>());
    }
  }
}