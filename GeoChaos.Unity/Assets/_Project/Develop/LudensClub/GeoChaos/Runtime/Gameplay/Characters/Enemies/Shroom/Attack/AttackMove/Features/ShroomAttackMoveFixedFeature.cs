using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Attack.AttackMove;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Shroom.Attack.AttackMove
{
  public class ShroomAttackMoveFixedFeature : AttackMoveFixedFeature<ShroomTag>
  {
    public ShroomAttackMoveFixedFeature(IEcsSystemFactory systems) : base(systems)
    {
    }
  }
}