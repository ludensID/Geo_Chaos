using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Detection
{
  public class FrogDetectionFeature : EcsFeature
  {
    public FrogDetectionFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<DeleteFrogWasAimedSystem>());
      Add(systems.Create<RememberAimedFrogSystem>());
      
      Add(systems.Create<PrepareFrogToSelectionSystem>());
      Add(systems.Create<AimFrogOnTargetInViewSystem>());
      Add(systems.Create<AimFrogOnTargetInFrontSystem>());
      Add(systems.Create<AimFrogOnTargetInBackSystem>());
      Add(systems.Create<AimFrogSystem>());
    }
  }
}