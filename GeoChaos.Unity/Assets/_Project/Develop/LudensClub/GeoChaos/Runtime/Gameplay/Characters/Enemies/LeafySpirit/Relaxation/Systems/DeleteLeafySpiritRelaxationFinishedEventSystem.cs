using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Relaxation
{
  public class DeleteLeafySpiritRelaxationFinishedEventSystem : DeleteSystem<OnRelaxationFinished>
  {
    protected DeleteLeafySpiritRelaxationFinishedEventSystem(GameWorldWrapper gameWorldWrapper) 
      : base(gameWorldWrapper, x => x.Inc<LeafySpiritTag>())
    {
    }
  }
}