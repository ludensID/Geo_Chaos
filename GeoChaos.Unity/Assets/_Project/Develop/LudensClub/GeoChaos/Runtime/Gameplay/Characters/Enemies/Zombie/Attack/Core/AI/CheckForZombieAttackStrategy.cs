using LudensClub.GeoChaos.Runtime.Gameplay.Damage;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.AI.BehaviourTrees;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Attack
{
  public class CheckForZombieAttackStrategy : IConditionStrategy
  {
    public EcsEntity Entity { get; set; }
      
    public bool Check()
    {
      return Entity.Has<Aimed>() && !Entity.Has<AttackCooldown>() || Entity.Has<Attacking>();
    }
  }
}