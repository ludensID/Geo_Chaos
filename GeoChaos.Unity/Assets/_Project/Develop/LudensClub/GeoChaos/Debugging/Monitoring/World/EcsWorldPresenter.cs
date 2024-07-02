using System;
using System.Collections.Generic;
using System.Linq;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Debugging.Monitoring
{
  public class EcsWorldPresenter : IEcsWorldPresenter, IEcsWorldEventListener
  {
    private readonly IEcsWorldWrapper _wrapper;
    private readonly IEcsWorldViewFactory _viewFactory;
    private readonly IEcsEntityPresenterFactory _entityFactory;
    private readonly IEcsUniversePresenter _parent;
    private readonly List<IEcsEntityPresenter> _children = new();
    private readonly HashSet<int> _dirtyEntities = new();
    private Type[] _typesCache;
    private int[] _entities;

    public EcsWorldView View { get; private set; }

    public EcsWorldPresenter(IEcsWorldWrapper wrapper,
      IEcsWorldViewFactory viewFactory,
      IEcsEntityPresenterFactory entityFactory,
      IEcsUniversePresenter parent)
    {
      _wrapper = wrapper;
      _viewFactory = viewFactory;
      _entityFactory = entityFactory;
      _parent = parent;
    }

    public void Initialize()
    {
      View = _viewFactory.Create(_parent.View.transform);
      View.gameObject.name = $"[ECS-WORLD {_wrapper.Name}]";

      _wrapper.World.AddEventListener(this);

      var entities = new int[_wrapper.World.GetAllocatedEntitiesCount()];
      _wrapper.World.GetAllEntities(ref _entities);
      foreach (int entity in entities)
        OnEntityCreated(entity);
    }

    public void Tick()
    {
      var updatables = _children.Select(x => x.Entity).Except(_dirtyEntities);

      if (updatables.Count() + _dirtyEntities.Count != _children.Count)
        throw new ArgumentException();

      foreach (int updatable in updatables)
      {
#if UNITY_EDITOR && !DISABLE_PROFILING
        using (new Unity.Profiling.ProfilerMarker($"{_children[updatable].View.gameObject.name[..8]}.UpdateView()").Auto())
#endif
        {
          _children[updatable].UpdateView();
        }
      }

      foreach (int dirtyEntity in _dirtyEntities)
      {
#if UNITY_EDITOR && !DISABLE_PROFILING
        using (new Unity.Profiling.ProfilerMarker($"{_children[dirtyEntity].View.gameObject.name[..8]}.Tick()").Auto())
#endif
        {
          _children[dirtyEntity].Tick();
        }
      }

      _dirtyEntities.Clear();
    }

    public void OnEntityCreated(int entity)
    {
      if (_children.All(x => x.Entity != entity))
      {
        IEcsEntityPresenter instance = _entityFactory.Create(entity, this, _wrapper);
        instance.Initialize();
        _children.Add(instance);
        View.Entities.Add(instance.View);
        _dirtyEntities.Add(entity);
      }

      _children[entity].SetActive(true);
    }

    public void OnEntityChanged(int entity)
    {
      _dirtyEntities.Add(entity);
    }

    public void OnEntityDestroyed(int entity)
    {
      _children[entity].SetActive(false);
    }

    public void OnFilterCreated(EcsFilter filter)
    {
    }

    public void OnWorldResized(int newSize)
    {
    }

    public void OnWorldDestroyed(EcsWorld world)
    {
      _wrapper.World.RemoveEventListener(this);
    }
  }
}