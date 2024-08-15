using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Detection
{
  public class DeleteFrogWasTargetInViewSystem : DeleteSystem<WasTargetInView>
  {
    protected DeleteFrogWasTargetInViewSystem(GameWorldWrapper gameWorldWrapper) 
        : base(gameWorldWrapper, x => x.Inc<FrogTag>())
    {
    }
  }
}