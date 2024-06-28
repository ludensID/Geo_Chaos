using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.AI;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Creation.Components;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemies.Lama
{
  public class AddBoundsRefSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _initializedLamas;

    public AddBoundsRefSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _initializedLamas = _game
        .Filter<LamaTag>()
        .Inc<InitializeCommand>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity lama in _initializedLamas)
      {
        lama.Get<SpawnPointRef>().Spawn.GetComponent<PhysicalBoundsConverter>().Convert(lama);
      }
    }
  }
}