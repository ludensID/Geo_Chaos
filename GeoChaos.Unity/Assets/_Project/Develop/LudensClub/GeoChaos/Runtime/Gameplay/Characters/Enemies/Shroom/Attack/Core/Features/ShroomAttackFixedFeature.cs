using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Shroom.Attack.AttackMove;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Shroom.Attack
{
  public class ShroomAttackFixedFeature : EcsFeature
  {
    public ShroomAttackFixedFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<ShroomAttackMoveFixedFeature>());
    }
  }
}