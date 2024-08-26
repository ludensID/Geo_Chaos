using LudensClub.GeoChaos.Runtime.Gameplay.AI;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Patrol;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Wait;
using LudensClub.GeoChaos.Runtime.Infrastructure.AI.BehaviourTrees;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Lama.Patrol
{
  public class AddLamaPatrolCommandStrategy : ActionStrategy<Patrolling, PatrolCommand>, IResetStrategy
  {
    public void Reset()
    {
      if (Entity.Has<Patrolling>())
        Entity.Add<StopPatrolCommand>();
    }
  }
}