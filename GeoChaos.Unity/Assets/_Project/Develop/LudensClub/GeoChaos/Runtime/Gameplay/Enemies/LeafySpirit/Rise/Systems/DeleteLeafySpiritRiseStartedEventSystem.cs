using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemies.LeafySpirit.Rise
{
  public class DeleteLeafySpiritRiseStartedEventSystem : Delete<OnRiseStarted>
  {
    protected DeleteLeafySpiritRiseStartedEventSystem(GameWorldWrapper gameWorldWrapper) 
        : base(gameWorldWrapper, x => x.Inc<LeafySpiritTag>())
    {
    }
  }
}