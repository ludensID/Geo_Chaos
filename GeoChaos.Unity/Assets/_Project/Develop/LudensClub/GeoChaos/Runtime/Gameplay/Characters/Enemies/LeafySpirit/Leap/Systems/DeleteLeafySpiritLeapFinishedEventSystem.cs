using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Leap
{
  public class DeleteLeafySpiritLeapFinishedEventSystem : Delete<OnLeapFinished>
  {
    protected DeleteLeafySpiritLeapFinishedEventSystem(GameWorldWrapper gameWorldWrapper) 
      : base(gameWorldWrapper,  x=> x.Inc<LeafySpiritTag>())
    {
    }
  }
}