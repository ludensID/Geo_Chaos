using System.Collections.Generic;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Props;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core.Destroying
{
  public class DestroyEntitySystem : IEcsRunSystem
  {
    private readonly List<IPushable> _pools;
    private readonly EcsWorld _game;
    private readonly EcsEntities _destroyingEntities;

    public DestroyEntitySystem(GameWorldWrapper gameWorldWrapper, List<IPushable> pools)
    {
      _pools = pools;
      _game = gameWorldWrapper.World;
      
      _destroyingEntities = _game
        .Filter<Destroying>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity destroying in _destroyingEntities)
      {
        if (destroying.Has<ViewRef>())
        {
          BaseView view = destroying.Get<ViewRef>().View;
          if (destroying.Has<Poolable>())
            _pools.Find(x => x.HasId(destroying.Get<EntityId>().Id)).Push(view);
          else
            Object.Destroy(view.gameObject);
        }

        destroying.Dispose();
      }
    }
  }
}