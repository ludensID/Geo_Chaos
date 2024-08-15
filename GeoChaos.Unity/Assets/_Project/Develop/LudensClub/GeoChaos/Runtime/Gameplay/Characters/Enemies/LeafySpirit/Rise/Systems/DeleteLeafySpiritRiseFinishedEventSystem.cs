using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Rise
{
  public class DeleteLeafySpiritRiseFinishedEventSystem : DeleteSystem<OnRiseFinished>
  {
    protected DeleteLeafySpiritRiseFinishedEventSystem(GameWorldWrapper gameWorldWrapper) 
      : base(gameWorldWrapper, x => x.Inc<LeafySpiritTag>())
    {
    }
  }
}