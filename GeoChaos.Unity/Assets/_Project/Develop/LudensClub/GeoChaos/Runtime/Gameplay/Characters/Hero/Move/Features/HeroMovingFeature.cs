using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Move
{
  public class HeroMovingFeature : EcsFeature
  {
    public HeroMovingFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<Delete<MoveHeroCommand>>());
      Add(systems.Create<ReadMovementSystem>());
      Add(systems.Create<SowMoveCommandSystem>());
      Add(systems.Create<InterruptMovementSystem>());
      Add(systems.Create<CalculateHeroMovementVectorSystem>());
    }
  }
}