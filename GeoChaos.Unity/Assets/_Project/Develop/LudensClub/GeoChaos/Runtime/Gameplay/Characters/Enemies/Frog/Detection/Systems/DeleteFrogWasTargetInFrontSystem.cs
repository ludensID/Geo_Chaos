using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Detection
{
  public class DeleteFrogWasTargetInFrontSystem : DeleteSystem<WasTargetInFront>
  {
    protected DeleteFrogWasTargetInFrontSystem(GameWorldWrapper gameWorldWrapper) 
      : base(gameWorldWrapper, x=> x.Inc<FrogTag>())
    {
    }
  }
}