using System;
using System.Linq;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Debugging.Monitoring
{
  public class EcsEntityPresenter : IEcsEntityPresenter
  {
    private const string ENTITY_FORMAT = "D8";

    private readonly IEcsWorldWrapper _wrapper;
    private readonly IEcsEntityViewFactory _viewFactory;
    private readonly IEcsWorldPresenter _parent;
    private Type[] _typesCache;
    private object[] _components;
    private int _componentCount;
    private IEcsPool[] _pools;
    private IEcsPool[] _valuePools;

    public int Entity { get; }
    public EcsEntityView View { get; private set; }

    public EcsEntityPresenter(int entity,
      IEcsWorldWrapper wrapper,
      IEcsEntityViewFactory viewFactory,
      IEcsWorldPresenter parent)
    {
      _wrapper = wrapper;
      _viewFactory = viewFactory;
      _parent = parent;
      Entity = entity;
    }

    public void Initialize()
    {
      View = _viewFactory.Create(_parent.View.transform);
      View.SetController(this);
    }

    public void Tick()
    {
      var entityName = Entity.ToString(ENTITY_FORMAT);
      if (_wrapper.World.GetEntityGen(Entity) > 0)
      {
        entityName = UpdateName(entityName);

        UpdateView();
      }
      else
      {
        View.Components.Clear();
        _componentCount = 0;
      }

      View.gameObject.name = entityName;
    }

    private string UpdateName(string entityName)
    {
      int typeCount = _wrapper.World.GetComponentTypes(Entity, ref _typesCache);
      for (var i = 0; i < typeCount; i++)
      {
        entityName = $"{entityName}:{EcsMonitoring.GetCleanGenericTypeName(_typesCache[i])}";
      }

      return entityName;
    }

    public void UpdateView()
    {
      _componentCount = _wrapper.World.GetComponents(Entity, ref _components);
      Resize();

      if (View.Components.Count != _componentCount)
        throw new IndexOutOfRangeException();

      for (int i = 0; i < _componentCount; i++)
      {
        EcsComponentView componentView = View.Components[i];
        object component = _components[i];
        if (componentView.Value == null || !componentView.Value.Equals(component))
          componentView.Value = (IEcsComponent)component;
      }
    }

    private void UpdatePools()
    {
      int count = _wrapper.World.GetAllPools(ref _pools);
      _valuePools = new IEcsPool[count];
      Array.Copy(_pools, _valuePools, count);
    }

    private void Resize()
    {
      int viewCount = View.Components.Count;
      if (_componentCount < viewCount)
      {
        View.Components.RemoveRange(_componentCount, viewCount - _componentCount);
      }
      else if (_componentCount > viewCount)
      {
        for (int i = viewCount; i < _componentCount; i++)
          View.Components.Add(new EcsComponentView());
      }

      if (View.Components.Count != _componentCount)
        throw new IndexOutOfRangeException();
    }

    public void SetActive(bool value)
    {
      if (View)
        View.gameObject.SetActive(value);
    }

    public void AddComponents()
    {
      UpdatePools();
      foreach (EcsComponentView component in View.ComponentPull)
      {
        IEcsPool pool = _valuePools.First(x => x.GetComponentType().Name == component.Value.GetType().Name);
        if (pool.Has(Entity))
          pool.SetRaw(Entity, component.Value);
        else
          pool.AddRaw(Entity, component.Value);
      }
    }

    public void RemoveComponents()
    {
      UpdatePools();
      foreach (EcsComponentView component in View.ComponentPull)
      {
        IEcsPool pool = _valuePools.First(x => x.GetComponentType().Name == component.Value.GetType().Name);
        if (pool.Has(Entity))
          pool.Del(Entity);
      }
    }
  }
}