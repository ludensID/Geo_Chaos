using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Aim;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Hook;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero
{
  public class HeroLateFeature : EcsFeature
  {
    public HeroLateFeature(IEcsSystemFactory systems) 
    {
      Add(systems.Create<MoveCameraSystem>());
        
      Add(systems.Create<HeroHookLateFeature>());
      
      Add(systems.Create<HeroAimLateFeature>());
    }
  }
}