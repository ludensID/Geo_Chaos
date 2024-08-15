using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Correction
{
  public class DeleteLeafySpiritShouldMoveCommand : DeleteSystem<ShouldMoveCommand>
  {
    protected DeleteLeafySpiritShouldMoveCommand(GameWorldWrapper gameWorldWrapper) 
      : base(gameWorldWrapper, x => x.Inc<LeafySpiritTag>())
    {
    }
  }
}