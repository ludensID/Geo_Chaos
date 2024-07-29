using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Lama.Watch
{
  public class LamaWatchingFeature : EcsFeature
  {
    public LamaWatchingFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<LamaWatchingSystem>());
      Add(systems.Create<DeleteLamaWatchCommandSystem>());
      Add(systems.Create<StopLamaWatchingSystem>());     
      Add(systems.Create<DeleteLamaStopWatchCommandSystem>());
    }
  }
}