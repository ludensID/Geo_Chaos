using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Jump;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Jump
{
  public class HeroJumpFeature : EcsFeature
  {
    public HeroJumpFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<FallHeroSystem>());
      Add(systems.Create<LandHeroSystem>());

      Add(systems.Create<SetJumpHorizontalSpeedSystem>());

      Add(systems.Create<ReadInputForHeroJumpSystem>());
      Add(systems.Create<InterruptHeroJumpSystem>());
      Add(systems.Create<ConvertDelayedToJustComponentSystem<DelayJumpCommand, JumpCommand>>());
      Add(systems.Create<ReadJumpDelayedInputSystem>());
      Add(systems.Create<JumpHeroSystem>());
      Add(systems.Create<FilterJumpStopCommandSystem>());
      Add(systems.Create<StopHeroJumpSystem>());
    }
  }
}