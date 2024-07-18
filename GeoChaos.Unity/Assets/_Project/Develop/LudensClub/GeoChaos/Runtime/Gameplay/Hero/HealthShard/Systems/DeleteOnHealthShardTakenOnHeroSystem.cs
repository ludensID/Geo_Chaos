using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.HealthShard
{
  public class DeleteOnHealthShardTakenOnHeroSystem : Delete<OnHealthShardTaken>
  {
    protected DeleteOnHealthShardTakenOnHeroSystem(GameWorldWrapper gameWorldWrapper) 
      : base(gameWorldWrapper, x => x.Inc<HeroTag>())
    {
    }
  }
}