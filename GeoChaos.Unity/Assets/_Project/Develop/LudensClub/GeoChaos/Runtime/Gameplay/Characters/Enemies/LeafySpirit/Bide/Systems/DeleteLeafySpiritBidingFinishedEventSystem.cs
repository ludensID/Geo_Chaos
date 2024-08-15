using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Bide
{
  public class DeleteLeafySpiritBidingFinishedEventSystem : DeleteSystem<OnBidingFinished>
  {
    protected DeleteLeafySpiritBidingFinishedEventSystem(GameWorldWrapper gameWorldWrapper) 
        : base(gameWorldWrapper, x => x.Inc<LeafySpiritTag>())
    {
    }
  }
}