using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Shoot.Aim;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.View;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Features
{
  public class HeroLateFeature : EcsFeature
  {
    public HeroLateFeature(IEcsSystemFactory systems) 
    {
      Add(systems.Create<PrecastHookViewSystem>());
      Add(systems.Create<DrawHookViewSystem>());
      
      Add(systems.Create<DrawAimLineSystem>());
    }
  }
}