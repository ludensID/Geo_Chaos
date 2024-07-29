using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Lama.Attack
{
  public class LamaAttackFeature : EcsFeature
  {
    public LamaAttackFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<CheckForLamaReadyAttackSystem>());

      Add(systems.Create<DeleteLamaAttackEventsSystem>());

      Add(systems.Create<StartLamaAttackSystem>());
      Add(systems.Create<DeleteLamaAttackCommandSystem>());

      Add(systems.Create<CheckLamaForHitCooldownExpiredSystem>());
      Add(systems.Create<LamaHitSystem>());
      Add(systems.Create<DeleteLamaBiteCommandSystem>());

      Add(systems.Create<CheckLamaForHitTimerExpiredSystem>());
      Add(systems.Create<CheckLamaForComboHitSystem>());
      Add(systems.Create<AddLamaHitCooldownSystem>());

      Add(systems.Create<CheckLamaForComboCooldownExpiredSystem>());
      Add(systems.Create<FinishLamaComboAttackSystem>());

      Add(systems.Create<StopLamaAttackSystem>());
      Add(systems.Create<DeleteLamaStopAttackCommandSystem>());

      Add(systems.Create<DamageFromLamaAttackSystem>());
    }
  }
}