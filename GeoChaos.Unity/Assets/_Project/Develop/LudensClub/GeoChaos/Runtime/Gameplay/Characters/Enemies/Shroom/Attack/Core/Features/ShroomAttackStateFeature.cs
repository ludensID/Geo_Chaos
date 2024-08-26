using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Attack.State;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Shroom.Attack
{
  public class ShroomAttackStateFeature : AttackStateFeature<ShroomTag>
  {
    public ShroomAttackStateFeature(IEcsSystemFactory systems) : base(systems)
    {
    }
  }
}