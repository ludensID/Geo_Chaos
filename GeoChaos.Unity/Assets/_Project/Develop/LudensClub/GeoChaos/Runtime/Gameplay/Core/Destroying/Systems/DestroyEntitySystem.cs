using System.Collections.Generic;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Props;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core.Destroying
{
  public class DestroyEntitySystem : IEcsRunSystem
  {
    private readonly List<IPushable> _pushables;
    private readonly EcsWorld _game;
    private readonly EcsEntities _destroyings;

    public DestroyEntitySystem(GameWorldWrapper gameWorldWrapper, List<IPushable> pushables)
    {
      _pushables = pushables;
      _game = gameWorldWrapper.World;
      _destroyings = _game
        .Filter<Destroying>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity destroying in _destroyings)
      {
        if (destroying.Has<ViewRef>())
        {
          BaseView view = destroying.Get<ViewRef>().View;
          if (destroying.Has<Poolable>())
            _pushables.Find(x => x.HasId(destroying.Get<EntityId>().Id)).Push(view);
          else
            Object.Destroy(view);
        }

        destroying.Dispose();
      }
    }
  }
}