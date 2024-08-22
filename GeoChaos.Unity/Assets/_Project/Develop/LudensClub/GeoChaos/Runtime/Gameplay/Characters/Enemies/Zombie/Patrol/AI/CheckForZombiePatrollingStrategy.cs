using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Patrol;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Wait;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Watch;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.AI.BehaviourTrees;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Patrol
{
  public class CheckForZombiePatrollingStrategy : IConditionStrategy
  {
    public EcsEntity Entity { get; set; }

    public bool Check()
    {
      return !Entity.Has<Aimed>() && !Entity.Has<WatchingTimer>()
        && !Entity.Has<OnPatrolFinished>() && !Entity.Has<WaitingTimer>();
    }
  }
}