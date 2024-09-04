using System;
using System.Linq;
using Cysharp.Text;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Debugging.Monitoring.Sorting;
using LudensClub.GeoChaos.Runtime;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;
using Unity.Profiling;
using UnityEditor;

namespace LudensClub.GeoChaos.Debugging.Monitoring
{
  public class EcsEntityPresenter : IEcsEntityPresenter
  {
    private const string ENTITY_FORMAT = "D8";
    private const string TRY_UPDATE_NAME = nameof(TryUpdateName) + "()";

    private static readonly Type _componentType = typeof(IEcsComponent);

    private readonly SpecifiedClosure<IEcsComponentView, IEcsPool> _poolClosure
      = new SpecifiedClosure<IEcsComponentView, IEcsPool>((x, pool) => x.Pool == pool);

    private readonly IEcsWorldWrapper _wrapper;
    private readonly IEcsEntityViewFactory _viewFactory;
    private readonly IEcsComponentViewFactory _componentFactory;
    private readonly IEcsWorldPresenter _parent;
    private readonly IEcsComponentSorter _sorter;

    private int _componentCount;
    private string _name;
    private string _entityWithDelimiter;
    private string _gameObjectName;
    private bool _isNameWithComponent;
    private IEcsComponentView _namedComponentView;

    public int Entity { get; }
    public string EntityString { get; }
    public EcsEntityView View { get; private set; }

    public EcsEntityPresenter(int entity,
      IEcsWorldWrapper wrapper,
      IEcsEntityViewFactory viewFactory,
      IEcsComponentViewFactory componentFactory,
      IEcsWorldPresenter parent,
      IEcsComponentSorter sorter)
    {
      _wrapper = wrapper;
      _viewFactory = viewFactory;
      _componentFactory = componentFactory;
      _parent = parent;
      _sorter = sorter;
      Entity = entity;
      EntityString = Entity.ToString(ENTITY_FORMAT);
      _entityWithDelimiter = EntityString + ":";
    }

    public void Initialize()
    {
      View = _viewFactory.Create(_parent.View.transform);
      View.SetController(this);
      _gameObjectName = EntityString;
      View.gameObject.name = _gameObjectName;
    }

    public void Tick()
    {
      UpdateView();

#if !DISABLE_PROFILING
      using (new ProfilerMarker(TRY_UPDATE_NAME).Auto())
#endif
      {
        if (TryUpdateName())
        {
          View.gameObject.name = _gameObjectName;
        }
      }
    }

    private bool TryUpdateName()
    {
      bool hasComponents = View.Components.Count > 0;
      if (!_isNameWithComponent && hasComponents)
      {
        _isNameWithComponent = true;
        _namedComponentView = View.Components[0];
        _gameObjectName = ZString.Concat(_entityWithDelimiter, _namedComponentView.Name);
        return true;
      }

      if (_isNameWithComponent && !hasComponents)
      {
        _isNameWithComponent = false;
        _namedComponentView = null;
        _gameObjectName = EntityString;
        return true;
      }

      if (_isNameWithComponent && _namedComponentView != View.Components[0])
      {
        _namedComponentView = View.Components[0];
        _gameObjectName = ZString.Concat(_entityWithDelimiter, _namedComponentView.Name);
        return true;
      }

      return false;
    }

    public void UpdateView()
    {
      if (_wrapper.World.GetEntityGen(Entity) < 0)
      {
        View.Components.Clear();
        _componentCount = 0;
        return;
      }

#if !DISABLE_PROFILING
      using (new ProfilerMarker("PrepareComponents()").Auto())
#endif
      {
        _componentCount = _wrapper.World.GetComponentsCount(Entity);
      }

#if !DISABLE_PROFILING
      using (new ProfilerMarker("Resize()").Auto())
#endif
      {
        Resize();
      }

#if !DISABLE_PROFILING
      using (new ProfilerMarker("Sort()").Auto())
#endif
      {
        View.Components.Sort(_sorter.EcsComponentViewComparator);
      }

      EditorUtility.SetDirty(View);
    }

    private void Resize()
    {
#if !DISABLE_PROFILING
      using (new ProfilerMarker("Remove()").Auto())
#endif
      {
        for (int i = 0; i < View.Components.Count; i++)
        {
          View.Components[i].Update();
          if (!View.Components[i].HasValue)
            View.Components.RemoveAt(i--);
        }
      }

#if !DISABLE_PROFILING
      using (new ProfilerMarker("Add()").Auto())
#endif
      {
        foreach (IEcsPool pool in _parent.Pools)
        {
          if (pool.Has(Entity) && !View.Components.AnyNonAlloc(_poolClosure.SpecifyPredicate(pool)))
          {
            Type componentType = pool.GetComponentType();
            IEcsComponentView componentView =
              _componentFactory.Create(componentType, Entity, pool);
            componentView.Name = EditorContext.GetPrettyName(componentType, _componentType);
            componentView.Update();
            View.Components.Add(componentView);
          }
        }
      }

      if (View.Components.Count != _componentCount)
        throw new IndexOutOfRangeException(
          $"View component count ({View.Components.Count}) is not equal to component count ({_componentCount})");
    }

    public void SetActive(bool value)
    {
      if (View)
        View.gameObject.SetActive(value);
    }

    public void AddComponents()
    {
      foreach (EcsComponentView component in View.ComponentPull)
      {
        _name = EditorContext.GetPrettyName(component.Value, _componentType);
        IEcsPool pool = _parent.Pools.FirstOrDefault(x => EditorContext.GetPrettyName(x.GetComponentType(), _componentType) == _name)
          ?? _wrapper.World.GetPoolEnsure(component.Value.GetType());

        if (pool.Has(Entity))
          pool.SetRaw(Entity, component.Value);
        else
          pool.AddRaw(Entity, component.Value);
      }
    }

    public void RemoveComponents()
    {
      foreach (EcsComponentView component in View.ComponentPull)
      {
        _name = EditorContext.GetPrettyName(component.Value, _componentType);
        IEcsPool pool = _parent.Pools.First(x => EditorContext.GetPrettyName(x.GetComponentType(), _componentType) == _name);
        if (pool.Has(Entity))
          pool.Del(Entity);
      }
    }

    public void ChangeComponent(IEcsComponent component)
    {
      IEcsPool pool = _parent.Pools.First(x => x.GetComponentType() == component.GetType());
      pool.SetRaw(Entity, component);
    }
  }
}