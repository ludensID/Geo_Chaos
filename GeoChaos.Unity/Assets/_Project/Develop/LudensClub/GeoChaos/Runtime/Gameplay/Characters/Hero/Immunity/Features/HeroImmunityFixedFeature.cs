using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Immunity
{
  public class HeroImmunityFixedFeature : EcsFeature
  {
    public HeroImmunityFixedFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<DisableImmunityColliderSystem>());
      Add(systems.Create<EnableImmunityColliderSystem>());
    }
  }
}