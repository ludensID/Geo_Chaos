using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Attack.Cooldown
{
  public class AttackCooldownFeature<TFilterComponent> : EcsFeature
  where TFilterComponent : struct, IEcsComponent
  {
    public AttackCooldownFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<DeleteExpiredAttackCooldownSystem<TFilterComponent>>());
      Add(systems.Create<StartAttackCooldownSystem<TFilterComponent>>());
    }
  }
}