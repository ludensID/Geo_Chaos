using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Creation.SpawnPoint;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Creation
{
  public class CreationFeature : EcsFeature
  {
    public CreationFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<SpawnPointFeature>());

      Add(systems.Create<DeleteSystem<OnConverted>>());
      Add(systems.Create<CreateEntityWithViewSystem>());

      Add(systems.Create<MoveToSpawnEntitySystem>());
    }
  }
}