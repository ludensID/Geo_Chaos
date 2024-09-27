using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Creation.SpawnPoint
{
  public class SpawnPointFeature : EcsFeature
  {
    public SpawnPointFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<RestartSpawnSystem>());
      Add(systems.Create<SpawnSystem>());
    }
  }
}