using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Aim;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Attack;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Bump;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Dash;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.HealthShard;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Immunity;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Initialize;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Interaction;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Jump;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Move;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Shoot;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Systems;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero
{
  public class HeroFeature : EcsFeature
  {
    public HeroFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<HeroInitializingFeature>());
      Add(systems.Create<HeroBumpFeature>());

      Add(systems.Create<ReadViewDirectionInputSystem>());

      Add(systems.Create<HeroMovingFeature>());
      Add(systems.Create<HeroJumpFeature>());
      Add(systems.Create<HeroDashFeature>());
      Add(systems.Create<HeroAttackFeature>());
      Add(systems.Create<HeroHookFeature>());
      Add(systems.Create<HeroAimFeature>());
      Add(systems.Create<HeroShootFeature>());
      Add(systems.Create<HeroInteractionFeature>());
      
      Add(systems.Create<HeroImmunityFeature>());
      
      Add(systems.Create<HeroHealthShardFeature>());
      
      Add(systems.Create<SetHeroBodyDirectionSystem>());
      
    }
  }
}