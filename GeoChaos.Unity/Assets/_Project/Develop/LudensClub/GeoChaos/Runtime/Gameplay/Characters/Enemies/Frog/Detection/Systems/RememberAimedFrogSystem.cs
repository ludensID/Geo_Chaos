using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Detection;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Detection
{
  public class RememberAimedFrogSystem : RememberAimedEntitySystem<FrogTag>
  {
    public RememberAimedFrogSystem(GameWorldWrapper gameWorldWrapper) : base(gameWorldWrapper)
    {
    }
  }
}