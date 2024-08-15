using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Detection;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Detection
{
  public class RememberFrogTargetInViewSystem : AddSystem<WasTargetInView, FrogTag>
  {
    public RememberFrogTargetInViewSystem(GameWorldWrapper worldWrapper) 
        : base(worldWrapper, x => x.Inc<TargetInView>())
    {
    }
  }
}