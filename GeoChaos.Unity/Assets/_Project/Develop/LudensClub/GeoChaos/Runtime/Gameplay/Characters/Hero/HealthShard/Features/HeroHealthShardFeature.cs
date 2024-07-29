using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.HealthShard
{
  public class HeroHealthShardFeature : EcsFeature
  {
    public HeroHealthShardFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<CheckHealthShardCountSystem>());
      Add(systems.Create<CalculateHeroHealthSystem>());
      Add(systems.Create<DeleteHeroHealthShardTakenEventSystem>());
    }
  }
}