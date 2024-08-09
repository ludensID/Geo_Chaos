using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Patrol
{
  public class FrogPatrolFeature : EcsFeature
  {
    public FrogPatrolFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<DeleteFrogPatrolStartedEventSystem>());
      Add(systems.Create<SelectFrogPatrolPointSystem>());
      Add(systems.Create<DeleteFrogPatrolCommandSystem>());
      
      Add(systems.Create<PrepareFrogJumpDuringPatrollingSystem>());
      Add(systems.Create<SyncFrogBodyDirectionWithPatrollingDirectionSystem>());
      
      Add(systems.Create<AfterFrogJumpSystem>());
      Add(systems.Create<DeleteFrogPatrolFinishedEventSystem>());
      Add(systems.Create<FinishFrogPatrollingSystem>());
        
      Add(systems.Create<StopFrogPatrollingSystem>());
    }
  }
}