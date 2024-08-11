using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Hook
{
  public class HeroHookLateFeature : EcsFeature
  {
    public HeroHookLateFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<PrecastHookViewSystem>());
      Add(systems.Create<DrawHookViewSystem>());      
    }
  }
}