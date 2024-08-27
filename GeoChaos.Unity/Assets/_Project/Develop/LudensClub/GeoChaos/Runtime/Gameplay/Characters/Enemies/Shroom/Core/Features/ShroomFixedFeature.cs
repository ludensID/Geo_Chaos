using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Shroom.Attack;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Shroom.Patrol;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Shroom
{
  public class ShroomFixedFeature : EcsFeature
  {
    public ShroomFixedFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<ShroomPatrolFixedFeature>());
      Add(systems.Create<ShroomAttackFixedFeature>());
    }
  }
}