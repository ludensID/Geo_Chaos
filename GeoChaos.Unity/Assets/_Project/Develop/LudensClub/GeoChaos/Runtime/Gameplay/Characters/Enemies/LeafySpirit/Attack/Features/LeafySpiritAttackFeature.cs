using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Attack
{
  public class LeafySpiritAttackFeature : EcsFeature
  {
    public LeafySpiritAttackFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<StartAttackByLeafySpiritSystem>());
      Add(systems.Create<CheckForLeafySpiritCooldownExpiredSystem>());
      Add(systems.Create<ShootLeafByLeafySpiritSystem>());
      Add(systems.Create<DeleteLeafySpiritAttackFinishedEventSystem>());
      Add(systems.Create<FinishAttackByLeafySpiritSystem>());
      Add(systems.Create<StopAttackByLeafySpiritSystem>());
    }
  }
}