using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.HealthShard
{
  public class DeleteHeroHealthShardTakenEventSystem : Delete<OnHealthShardTaken>
  {
    protected DeleteHeroHealthShardTakenEventSystem(GameWorldWrapper gameWorldWrapper) 
      : base(gameWorldWrapper, x => x.Inc<HeroTag>())
    {
    }
  }
}