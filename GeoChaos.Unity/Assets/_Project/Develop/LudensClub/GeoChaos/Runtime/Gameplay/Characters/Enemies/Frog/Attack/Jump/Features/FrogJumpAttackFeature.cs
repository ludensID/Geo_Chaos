using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Attack.Jump
{
  public class FrogJumpAttackFeature : EcsFeature
  {
    public FrogJumpAttackFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<FinishFrogJumpAttackSystem>());
      
      Add(systems.Create<FrogJumpAttackSystem>());
      Add(systems.Create<StopFrogJumpAttackSystem>());
    }
  }
}