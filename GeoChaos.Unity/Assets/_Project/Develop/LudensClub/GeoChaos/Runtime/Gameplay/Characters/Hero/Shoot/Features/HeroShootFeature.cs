using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Shoot
{
  public class HeroShootFeature : EcsFeature
  {
    public HeroShootFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<CheckForHeroShootCooldownExpiredSystem>());
      Add(systems.Create<ReadShootInputSystem>());
      Add(systems.Create<SowShootCommandSystem>());
      Add(systems.Create<ShootSystem>());
      Add(systems.Create<DamageFromShardSystem>());
      Add(systems.Create<CheckForShardLifeTimeExpiredSystem>());
    }
  }
}