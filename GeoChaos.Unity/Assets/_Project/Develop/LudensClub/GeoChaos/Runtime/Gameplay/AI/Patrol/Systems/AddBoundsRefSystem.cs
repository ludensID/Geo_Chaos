using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Patrol;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Creation;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.AI.Patrol
{
  public class AddBoundsRefSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _convertedBrains;

    public AddBoundsRefSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _convertedBrains = _game
        .Filter<Brain>()
        .Inc<Spawned>()
        .Inc<OnConverted>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity brain in _convertedBrains)
      {
        ref Spawned spawned = ref brain.Get<Spawned>();
        if (spawned.Spawn.TryUnpackEntity(_game, out EcsEntity spawn) && spawn.Has<PhysicalBoundsRef>())
          brain.Add((ref PhysicalBoundsRef bounds) => bounds = spawn.Get<PhysicalBoundsRef>());
      }
    }
  }
}