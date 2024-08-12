using LudensClub.GeoChaos.Runtime.Gameplay.Environment.Tongue.Move;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.Tongue
{
  public class TongueFixedFeature : EcsFeature
  {
    public TongueFixedFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<TongueMovingFixedFeature>());
    } 
  }
}