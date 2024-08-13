using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Detection;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Detection
{
  public class DeleteLeafySpiritWasAimedSystem : Delete<WasAimed>
  {
    protected DeleteLeafySpiritWasAimedSystem(GameWorldWrapper gameWorldWrapper) 
      : base(gameWorldWrapper, x => x.Inc<LeafySpiritTag>())
    {
    }
  }
}