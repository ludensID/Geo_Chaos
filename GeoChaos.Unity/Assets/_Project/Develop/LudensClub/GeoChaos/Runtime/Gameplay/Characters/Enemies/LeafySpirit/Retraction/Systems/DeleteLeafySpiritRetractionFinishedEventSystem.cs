using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Retraction
{
  public class DeleteLeafySpiritRetractionFinishedEventSystem : DeleteSystem<OnRetractionFinished>
  {
    protected DeleteLeafySpiritRetractionFinishedEventSystem(GameWorldWrapper gameWorldWrapper) 
      : base(gameWorldWrapper, x => x.Inc<LeafySpiritTag>())
    {
    }
  }
}