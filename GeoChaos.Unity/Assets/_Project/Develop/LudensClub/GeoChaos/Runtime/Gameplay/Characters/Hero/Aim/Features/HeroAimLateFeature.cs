using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Aim
{
  public class HeroAimLateFeature : EcsFeature
  {
    public HeroAimLateFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<DrawAimLineSystem>());
    }
  }
}