using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Lama.View
{
  public class SetLamaBodyDirectionSystem : SetBodyDirectionByMovementSystem<LamaTag>
  {
    public SetLamaBodyDirectionSystem(GameWorldWrapper gameWorldWrapper) : base(gameWorldWrapper)
    {
    }
  }
}