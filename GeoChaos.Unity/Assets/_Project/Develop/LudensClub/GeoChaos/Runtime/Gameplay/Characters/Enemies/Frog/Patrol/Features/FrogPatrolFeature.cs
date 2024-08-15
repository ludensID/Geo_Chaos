using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Patrol
{
  public class FrogPatrolFeature : EcsFeature
  {
    public FrogPatrolFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<SelectFrogPatrolPointSystem>());
      
      Add(systems.Create<DeleteFrogPatrolFinishedEventSystem>());
      Add(systems.Create<FinishFrogPatrollingSystem>());
      
      Add(systems.Create<StopFrogPatrollingJumpCycleWhenBumpingSystem>());
      Add(systems.Create<StartFrogPatrollingJumpCycleAfterBumpingSystem>());

      Add(systems.Create<StopFrogPatrollingSystem>());
    }
  }
}