using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Correction
{
  public class DeleteLeafySpiritShouldAttackCommand : Delete<ShouldAttackCommand>
  {
    protected DeleteLeafySpiritShouldAttackCommand(GameWorldWrapper gameWorldWrapper) 
      : base(gameWorldWrapper, x => x.Inc<LeafySpiritTag>())
    {
    }
  }
}