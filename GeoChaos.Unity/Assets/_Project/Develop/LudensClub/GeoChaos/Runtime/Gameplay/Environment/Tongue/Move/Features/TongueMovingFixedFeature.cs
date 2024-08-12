using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.Tongue.Move
{
  public class TongueMovingFixedFeature : EcsFeature
  {
    public TongueMovingFixedFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<CheckForTongueReachedMovePointSystem>());
    }
  }
}