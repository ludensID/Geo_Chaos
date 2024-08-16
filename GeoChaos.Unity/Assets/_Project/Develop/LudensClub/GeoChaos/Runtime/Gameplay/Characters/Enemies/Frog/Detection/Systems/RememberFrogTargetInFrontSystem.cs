using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Detection
{
  public class RememberFrogTargetInFrontSystem : AddSystem<WasTargetInFront, FrogTag>
  {
    public RememberFrogTargetInFrontSystem(GameWorldWrapper worldWrapper) 
      : base(worldWrapper, x => x.Inc<TargetInFront>())
    {
    }
  }
}