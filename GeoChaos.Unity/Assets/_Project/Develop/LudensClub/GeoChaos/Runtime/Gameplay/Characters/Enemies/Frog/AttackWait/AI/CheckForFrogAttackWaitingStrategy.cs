using LudensClub.GeoChaos.Runtime.Gameplay.Damage;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.AI.BehaviourTrees;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.AttackWait
{
  public class CheckForFrogAttackWaitingStrategy : IConditionStrategy
  {
    public EcsEntity Entity { get; set; }

    public bool Check()
    {
      return Entity.Has<OnAttackFinished>() || Entity.Has<AttackWaitingTimer>();
    }
  }
}