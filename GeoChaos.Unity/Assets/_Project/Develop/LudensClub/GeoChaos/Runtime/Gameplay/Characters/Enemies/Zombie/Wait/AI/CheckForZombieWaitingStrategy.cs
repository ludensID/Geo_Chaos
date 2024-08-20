using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Patrol;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Wait;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.AI.BehaviourTrees;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Wait
{
  public class CheckForZombieWaitingStrategy : IConditionStrategy
  {
    public EcsEntity Entity { get; set; }
      
    public bool Check()
    {
      return Entity.Has<OnPatrolFinished>() || Entity.Has<WaitingTimer>();
    }
  }
}