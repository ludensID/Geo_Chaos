using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Creation.Components;
using LudensClub.GeoChaos.Runtime.Gameplay.Creation.Systems;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Creation.Feature
{
  public class CreationFeature : EcsFeature
  {
    public CreationFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<SpawnEntitiesSystem>());

      Add(systems.Create<Delete<OnConverted, GameWorldWrapper>>());
      Add(systems.Create<CreateViewByPrefabSystem>());
      Add(systems.Create<MoveToSpawnEntitySystem>());
      
      Add(systems.Create<CreateEntityWithViewSystem>());
    }
  }
}