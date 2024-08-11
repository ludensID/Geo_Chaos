using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Shoot;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Aim
{
  public class HeroAimFeature : EcsFeature
  {
    public HeroAimFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<Delete<OnAimStarted>>());
      Add(systems.Create<Delete<OnAimFinished>>());
      Add(systems.Create<ReadAimInputSystem>());
      Add(systems.Create<CheckForHeroOnGroundToAimSystem>());
      Add(systems.Create<SwitchAimSystem>());
      Add(systems.Create<Delete<StartAimCommand>>());
      Add(systems.Create<Delete<FinishAimCommand>>());

      Add(systems.Create<SetAimDirectionToViewDirectionSystem>());
      Add(systems.Create<ReadAimPositionSystem>());
      Add(systems.Create<ReadAimDirectionSystem>());
      Add(systems.Create<CheckForHeroShootAndViewDirectionMatchingSystem>());
    }
  }
}