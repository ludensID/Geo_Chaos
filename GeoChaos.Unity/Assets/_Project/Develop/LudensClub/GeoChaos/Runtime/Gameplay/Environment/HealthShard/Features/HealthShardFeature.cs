using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.HealthShard
{
  public class HealthShardFeature : EcsFeature
  {
    public HealthShardFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<TakeHealthShardByHeroSystem>());
    }
  }
}