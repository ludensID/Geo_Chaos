using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.Tongue.Move
{
  public class TongueMovingFeature : EcsFeature
  {
    public TongueMovingFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<TongueMovingSystem>());
    } 
  }
}