using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Wait
{
  public class LeafySpiritWaitFeature : EcsFeature
  {
    public LeafySpiritWaitFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<LeafySpiritWaitingSystem>());
      Add(systems.Create<StopLeafySpiritWaitingSystem>());
      
      Add(systems.Create<CheckLeafySpiritForWaitingTimerExpiredSystem>());
    }
  }
}