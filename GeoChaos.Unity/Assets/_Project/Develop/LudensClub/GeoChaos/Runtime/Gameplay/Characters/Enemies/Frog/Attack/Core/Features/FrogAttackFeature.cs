using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Attack.Bite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Attack.Jump;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Attack
{
  public class FrogAttackFeature : EcsFeature
  {
    public FrogAttackFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<FrogAttackSystem>());
      
      Add(systems.Create<DeleteFrogAttackFinishedEventSystem>());
      Add(systems.Create<FinishFrogAttackSystem>());
      
      Add(systems.Create<DeleteFrogAttackStoppedEventSystem>());
      Add(systems.Create<StopFrogAttackSystem>());
        
      Add(systems.Create<FrogJumpAttackFeature>());
      Add(systems.Create<FrogBiteFeature>());
    }
  }
}