using System;
using System.Linq;
using LudensClub.GeoChaos.Runtime.Characteristics.Components;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEditor.Experimental.GraphView;
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
  }
}