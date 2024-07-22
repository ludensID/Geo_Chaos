using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.HealthShard
{
  public class DeleteHeroHealthShardTakenEventSystem : Delete<OnHealthShardTaken>
  {
    protected DeleteHeroHealthShardTakenEventSystem(GameWorldWrapper gameWorldWrapper) 
      : base(gameWorldWrapper, x => x.Inc<HeroTag>())
    {
    }
  }
}