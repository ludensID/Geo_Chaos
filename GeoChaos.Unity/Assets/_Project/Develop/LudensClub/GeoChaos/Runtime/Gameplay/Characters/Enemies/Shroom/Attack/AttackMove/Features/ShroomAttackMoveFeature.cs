using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Attack.AttackMove;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Shroom.Attack.AttackMove
{
  public class ShroomAttackMoveFeature : EcsFeature
  {
    public ShroomAttackMoveFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<StartShroomAttackMoveSystem>());
      Add(systems.Create<AttackMoveFeature<ShroomTag>>());
      Add(systems.Create<IncreaseGasShotCounterSystem>());
    }
  }
}