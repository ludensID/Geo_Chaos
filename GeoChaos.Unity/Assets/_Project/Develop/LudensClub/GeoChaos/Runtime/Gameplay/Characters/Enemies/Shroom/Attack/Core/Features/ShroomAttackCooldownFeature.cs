using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Attack.Cooldown;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Shroom.Attack
{
  public class ShroomAttackCooldownFeature : AttackCooldownFeature<ShroomTag>
  {
    public ShroomAttackCooldownFeature(IEcsSystemFactory systems) : base(systems)
    {
    }
  }
}