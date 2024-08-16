using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Bump;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.AttackWait;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Detection;
using LudensClub.GeoChaos.Runtime.Gameplay.Damage;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.AI.BehaviourTrees;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Attack
{
  public class CheckForFrogAttackStrategy : IConditionStrategy
  {
    public EcsEntity Entity { get; set; }

    public bool Check()
    { 
      return !Entity.Has<OnAttackFinished>() && !Entity.Has<Bumping>()
        && (Entity.Has<TargetInFront>() && !Entity.Has<AttackWaitingTimer>() || Entity.Has<Attacking>());
    }
  }
}