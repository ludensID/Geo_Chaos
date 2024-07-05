using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;
using Unity.Profiling;

namespace LudensClub.GeoChaos.Debugging.Monitoring
{
  public class EcsEntityPresenter : IEcsEntityPresenter
  {
    private const string ENTITY_FORMAT = "D8";

    private readonly IEcsWorldWrapper _wrapper;
    private readonly IEcsEntityViewFactory _viewFactory;
    private readonly IEcsWorldPresenter _parent;
    private readonly EcsComponentNameComparer _ecsComparer = new EcsComponentNameComparer();
    private readonly EcsComponentViewComparer _viewComparer = new EcsComponentViewComparer();
    private readonly StringBuilder _builder = new StringBuilder();
    private readonly List<string> _componentNames = new List<string>(); 
    private Type[] _typesCache;
    private object[] _components;
    private int _componentCount;
    private IEcsPool[] _pools;
    private IEcsPool[] _valuePools;
    private string _name;

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
#if !DISABLE_PROFILING
        using (new ProfilerMarker(nameof(UpdateName) + "()").Auto())
#endif
        {
          entityName = UpdateName(entityName);
        }

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
      _builder.Clear().Append(entityName);
      for (var i = 0; i < typeCount; i++)
      {
        _builder
          .Append(":")
          .Append(EditorContext.GetPrettyName(_typesCache[i]));
      }

      return _builder.ToString();
    }

    public void UpdateView()
    {
#if !DISABLE_PROFILING
      using (new ProfilerMarker("PrepareComponents()").Auto())
#endif
      {
        _componentCount = _wrapper.World.GetComponentsCount(Entity);
        Array.Resize(ref _components, _componentCount);
        _componentCount = _wrapper.World.GetComponents(Entity, ref _components);
        _componentNames.Clear();
        _componentNames.Capacity = _componentCount;
        foreach (object component in _components)
          _componentNames.Add(EditorContext.GetPrettyName(component));
      }
#if !DISABLE_PROFILING
      using (new ProfilerMarker("Resize()").Auto())
#endif
      {
        Resize();
      }

      if (View.Components.Count != _componentCount)
        throw new IndexOutOfRangeException();

#if !DISABLE_PROFILING
      using (new ProfilerMarker("Update View()").Auto())
#endif
      {
        for (int i = 0; i < _componentCount; i++)
        {
          int index = View.Components.FindIndex(x => x.Name == _componentNames[i]);
          View.Components[index].Value = (IEcsComponent)_components[i];
        }
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
#if !DISABLE_PROFILING
      using (new ProfilerMarker("Remove()").Auto())
#endif
      {
        for (int i = 0; i < View.Components.Count; i++)
        {
          _ecsComparer.Obj = View.Components[i].Name;
          if (_componentNames.AllNonAlloc(_ecsComparer))
            View.Components.RemoveAt(i--);
        }
      }

#if !DISABLE_PROFILING
      using (new ProfilerMarker("Add()").Auto())
#endif
      {
        for (int i = 0; i < _componentCount; i++)
        {
          string componentName = EditorContext.GetPrettyName(_components[i]);
          _viewComparer.Obj = componentName;
          if (View.Components.AllNonAlloc(_viewComparer))
            View.Components.Add(new EcsComponentView { Value = (IEcsComponent)_components[i], Name = componentName });
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
      UpdatePools();
      foreach (EcsComponentView component in View.ComponentPull)
      {
        _name = EditorContext.GetPrettyName(component.Value);
        IEcsPool pool = _valuePools.First(x => EditorContext.GetPrettyName(x.GetComponentType()) == _name);
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
        _name = EditorContext.GetPrettyName(component.Value);
        IEcsPool pool = _valuePools.First(x => EditorContext.GetPrettyName(x.GetComponentType()) == _name);
        if (pool.Has(Entity))
          pool.Del(Entity);
      }
    }

    private class EcsComponentNameComparer : IPredicate<string>
    {
      public string Obj; 
        
      public bool Predicate(string obj)
      {
        return obj != Obj;
      }
    }
    
    private class EcsComponentViewComparer : IPredicate<EcsComponentView>
    {
      public string Obj; 
        
      public bool Predicate(EcsComponentView obj)
      {
        return obj.Name != Obj;
      }
    }
  }
}