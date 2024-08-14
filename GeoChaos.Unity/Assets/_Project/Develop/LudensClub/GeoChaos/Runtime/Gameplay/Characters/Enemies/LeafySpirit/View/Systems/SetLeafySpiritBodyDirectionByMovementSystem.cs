using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.View
{
  public class SetLeafySpiritBodyDirectionByMovementSystem : SetBodyDirectionByMovementSystem<LeafySpiritTag>
  {
    public SetLeafySpiritBodyDirectionByMovementSystem(GameWorldWrapper gameWorldWrapper) : base(gameWorldWrapper)
    {
    }
  }
}