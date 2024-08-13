using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Detection;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Detection
{
  public class DeleteFrogWasAimedSystem : Delete<WasAimed>
  {
    protected DeleteFrogWasAimedSystem(GameWorldWrapper gameWorldWrapper) 
      : base(gameWorldWrapper, x => x.Inc<FrogTag>())
    {
    }
  }
}