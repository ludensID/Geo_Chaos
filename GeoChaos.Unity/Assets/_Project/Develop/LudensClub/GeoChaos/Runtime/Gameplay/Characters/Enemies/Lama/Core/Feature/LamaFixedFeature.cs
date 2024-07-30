using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Lama.Patrol;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Lama.Watch;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Lama
{
  public class LamaFixedFeature : EcsFeature
  {
    public LamaFixedFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<CheckLamaForPatrollingTimerExpiredSystem>());
      Add(systems.Create<CheckLamaForLookingTimerExpiredSystem>());
      Add(systems.Create<CheckLamaForWatchingTimerExpiredSystem>());
    }
  }
}