using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Relaxation
{
  public class DeleteLeafySpiritRelaxationFinishedEventSystem : Delete<OnRelaxationFinished>
  {
    protected DeleteLeafySpiritRelaxationFinishedEventSystem(GameWorldWrapper gameWorldWrapper) 
      : base(gameWorldWrapper, x => x.Inc<LeafySpiritTag>())
    {
    }
  }
}