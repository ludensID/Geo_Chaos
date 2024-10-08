using System;
using System.Collections.Generic;
using System.Linq;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Editor.Monitoring.Entity;
using LudensClub.GeoChaos.Editor.Monitoring.Universe;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using Unity.Profiling;

namespace LudensClub.GeoChaos.Editor.Monitoring.World
{
  public class EcsWorldPresenter : IEcsWorldPresenter, IEcsWorldEventListener
  {
    private readonly IEcsWorldWrapper _wrapper;
    private readonly IEcsWorldViewFactory _viewFactory;
    private readonly IEcsEntityPresenterFactory _entityFactory;
    private readonly IEcsUniversePresenter _parent;
    private readonly List<IEcsEntityPresenter> _children = new List<IEcsEntityPresenter>();
    private readonly HashSet<int> _dirtyEntities = new HashSet<int>();
    private readonly List<int> _updatables = new List<int>();
    private Type[] _typesCache;
    private int[] _entities;
    private IEcsPool[] _pools;

    public IEcsWorldWrapper Wrapper => _wrapper;
    public List<IEcsEntityPresenter> Children => _children;
    public EcsWorldView View { get; private set; }
    public IEcsPool[] Pools => _pools;

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
      UpdatePools();
        
      _updatables.Clear();
      _updatables.Capacity = _children.Count - _dirtyEntities.Count;

      foreach (IEcsEntityPresenter presenter in _children)
      {
        int entity = presenter.Entity;
        if (!_dirtyEntities.Contains(entity))
          _updatables.Add(entity);
      }

      foreach (int updatable in _updatables)
      {
#if !DISABLE_PROFILING
        using (new ProfilerMarker(EntityDictionary.GetString(updatable, EntityMethodType.UpdateView)).Auto())
#endif
        {
          _children[updatable].UpdateView();
        }
      }

      foreach (int dirtyEntity in _dirtyEntities)
      {
#if !DISABLE_PROFILING
        using (new ProfilerMarker(EntityDictionary.GetString(dirtyEntity, EntityMethodType.Tick)).Auto())
#endif
        {
          _children[dirtyEntity].Tick();
        }
      }

      _dirtyEntities.Clear();
    }
    
    private void UpdatePools()
    {
      _wrapper.World.GetAllPools(ref _pools);
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