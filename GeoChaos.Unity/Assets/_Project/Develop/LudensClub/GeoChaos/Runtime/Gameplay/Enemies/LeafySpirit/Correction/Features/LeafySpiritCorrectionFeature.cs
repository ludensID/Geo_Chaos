using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemies.LeafySpirit.Correction
{
  public class LeafySpiritCorrectionFeature : EcsFeature
  {
    public LeafySpiritCorrectionFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<DeleteLeafySpiritShouldMoveCommand>());
      Add(systems.Create<DeleteLeafySpiritShouldAttackCommand>());
      Add(systems.Create<CorrectLeafySpiritSystem>());
    }
  }
}