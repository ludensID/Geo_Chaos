using LudensClub.GeoChaos.Runtime.Gameplay.Environment.Tongue.Move;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.Tongue
{
  public class TongueFeature : EcsFeature
  {
    public TongueFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<TongueMovingFeature>());

      Add(systems.Create<StunFrogWhenTongueDiedSystem>());
    }
  }
}