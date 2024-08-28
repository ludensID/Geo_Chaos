using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Shroom.Attack.AttackMove;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Shroom.Attack.Reload;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Shroom.Attack
{
  public class ShroomAttackFeature : EcsFeature
  {
    public ShroomAttackFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<ShroomAttackStateFeature>());
      Add(systems.Create<ShroomAttackCooldownFeature>());
      
      Add(systems.Create<ReloadShroomFeature>());
      Add(systems.Create<ShroomAttackMoveFeature>());
      
      Add(systems.Create<DamageFromGasCloudBodySystem>());
    }
  }
}