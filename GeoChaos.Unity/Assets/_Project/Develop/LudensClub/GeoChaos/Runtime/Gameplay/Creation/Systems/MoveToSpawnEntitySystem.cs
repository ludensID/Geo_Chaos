using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core.Components;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Creation.Systems
{
  public class MoveToSpawnEntitySystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsFilter _spawnables;

    public MoveToSpawnEntitySystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _spawnables = _game
        .Filter<SpawnAvailable>()
        .Inc<ViewRef>()
        .End();
    }

    public void Run(EcsSystems systems)
    {
      foreach (var spawnable in _spawnables)
      {
        ref var spawn = ref _game.Get<SpawnAvailable>(spawnable);
        ref var viewRef = ref _game.Get<ViewRef>(spawnable);
        viewRef.View.transform.position = spawn.Position;
        viewRef.View.transform.rotation = spawn.Rotation;

        _game.Del<SpawnAvailable>(spawnable);
      }
    }
  }
}