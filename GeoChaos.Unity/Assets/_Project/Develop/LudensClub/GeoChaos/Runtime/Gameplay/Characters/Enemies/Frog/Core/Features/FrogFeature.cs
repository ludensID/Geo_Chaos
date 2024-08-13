using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Attack;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.AttackWait;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Chase;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Detection;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Jump;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.JumpBack;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.JumpCycle;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.JumpWait;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Patrol;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Turn;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Wait;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Watch;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog
{
  public class FrogFeature : EcsFeature
  {
    public FrogFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<InitializeFrogSystem>());
        
      Add(systems.Create<FrogDetectionFeature>());
      
      Add(systems.Create<FrogWaitFeature>());
      Add(systems.Create<FrogPatrolFeature>());
      Add(systems.Create<FrogChasingFeature>());
      Add(systems.Create<FrogAttackFeature>());
      Add(systems.Create<FrogAttackWaitFeature>());
      Add(systems.Create<FrogJumpBackFeature>());
      Add(systems.Create<FrogWatchFeature>());
      Add(systems.Create<FrogTurnFeature>());
      
      Add(systems.Create<FrogJumpCycleFeature>());
      Add(systems.Create<FrogJumpFeature>());
      Add(systems.Create<FrogJumpWaitFeature>());
    }
  }
}