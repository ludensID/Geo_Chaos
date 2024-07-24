using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.AI;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Creation;
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
        .Inc<OnConverted>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity lama in _initializedLamas)
      {
        ref Spawned spawned = ref lama.Get<Spawned>();
        if (spawned.Spawn.TryUnpackEntity(_game, out EcsEntity spawn))
          spawn.Get<ViewRef>().View.GetComponent<PhysicalBoundsConverter>().ConvertTo(lama);
      }
    }
  }
}