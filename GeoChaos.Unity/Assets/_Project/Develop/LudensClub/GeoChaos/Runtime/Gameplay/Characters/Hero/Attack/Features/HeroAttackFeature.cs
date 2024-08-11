using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Attack
{
  public class HeroAttackFeature : EcsFeature
  {
    public HeroAttackFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<DeleteHeroAttackStartedEventSystem>());
      Add(systems.Create<DeleteHeroAttackFinishedEventSystem>());
      Add(systems.Create<ReadAttackInputSystem>());
      Add(systems.Create<ResetComboCounterSystem>());
      Add(systems.Create<HeroAttackSystem>());
      Add(systems.Create<StopHeroAttackSystem>());
      Add(systems.Create<DamageFromHeroAttackSystem>());
        
      Add(systems.Create<HeroViewAttackSystem>());
      Add(systems.Create<SetHeroSwordViewColorSystem>());
    }
  }
}