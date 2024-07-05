using System;
using System.Linq;
using System.Text;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using Unity.Profiling;

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
    private StringBuilder _builder = new StringBuilder();

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
        _builder.Append($":{EcsMonitoring.GetCleanGenericTypeName(_typesCache[i])}");
      }

      return _builder.ToString();
    }

    public void UpdateView()
    {
#if !DISABLE_PROFILING
      using (new Unity.Profiling.ProfilerMarker("PrepareComponents()").Auto())
#endif
      {
        _componentCount = _wrapper.World.GetComponentsCount(Entity);
        Array.Resize(ref _components, _componentCount);
        _componentCount = _wrapper.World.GetComponents(Entity, ref _components);
      }
#if UNITY_EDITOR && !DISABLE_PROFILING
      using (new Unity.Profiling.ProfilerMarker("Resize()").Auto())
#endif
      {
        Resize();
      }

      if (View.Components.Count != _componentCount)
        throw new IndexOutOfRangeException();

#if UNITY_EDITOR && !DISABLE_PROFILING
      using (new Unity.Profiling.ProfilerMarker("Update View()").Auto())
#endif
      {
        for (int i = 0; i < _componentCount; i++)
        {
          int index = View.Components.FindIndex(x => x.Name == EditorContext.GetPrettyName(_components[i]));
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
          string viewComponentName = View.Components[i].Name;
          bool remove = true;
          for (int j = 0; j < _componentCount; j++)
          {
            if (EditorContext.GetPrettyName(_components[j]) == viewComponentName)
            {
              remove = false;
              break;
            }
          }
          if (remove)
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
          bool add = true;
          for (int j = 0; j < View.Components.Count; j++)
          {
            if (View.Components[j].Name == componentName)
            {
              add = false;
              break;
            }
          }
          if (add)
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