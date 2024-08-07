﻿using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Text;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Editor.General;
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
    private const string UPDATE_VIEW = nameof(UpdateName) + "()";

    private readonly IEcsWorldWrapper _wrapper;
    private readonly IEcsEntityViewFactory _viewFactory;
    private readonly IEcsWorldPresenter _parent;
    private readonly EcsComponentNameNegativeComparer _ecsNegativeComparer = new EcsComponentNameNegativeComparer();
    private readonly EcsComponentViewNegativeComparer _viewNegativeComparer = new EcsComponentViewNegativeComparer();
    private readonly EcsComponentViewComparer _viewComparer = new EcsComponentViewComparer();
    private readonly List<string> _componentNames = new List<string>();
    private EcsUniverseConfig _config;
    private Type[] _typesCache;
    private object[] _components;
    private int _componentCount;
    private IEcsPool[] _pools;
    private IEcsPool[] _valuePools;
    private string _name;

    public int Entity { get; }
    public string EntityString { get; }
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
      EntityString = Entity.ToString(ENTITY_FORMAT);
    }

    public void Initialize()
    {
      View = _viewFactory.Create(_parent.View.transform);
      View.SetController(this);
    }

    public void Tick()
    {
      string entityName = EntityString;
      if (_wrapper.World.GetEntityGen(Entity) > 0)
      {
        UpdateView();

#if !DISABLE_PROFILING
        using (new ProfilerMarker(UPDATE_VIEW).Auto())
#endif
        {
          entityName = UpdateName();
        }
      }
      else
      {
        View.Components.Clear();
        _componentCount = 0;
      }

      View.gameObject.name = entityName;
    }

    private string UpdateName()
    {
      using Utf16ValueStringBuilder builder = ZString.CreateStringBuilder();
      builder.Append(EntityString);
      if (View.Components.Count > 0)
      {
        builder.Append(":");
        builder.Append(View.Components[0].Name);
      }

#if !DISABLE_PROFILING
      using ProfilerMarker.AutoScope marker = new ProfilerMarker(nameof(ToString)).Auto();
#endif
      return builder.ToString();
    }

    public void UpdateView()
    {
#if !DISABLE_PROFILING
      using (new ProfilerMarker("PrepareComponents()").Auto())
#endif
      {
        _componentCount = _wrapper.World.GetComponentsCount(Entity);
        Array.Resize(ref _components, _componentCount);

#if !DISABLE_PROFILING
        using (new ProfilerMarker("GetComponents()").Auto())
#endif
        {
          _componentCount = _wrapper.World.GetComponents(Entity, ref _components);
        }

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
          _viewComparer.Obj = _componentNames[i];
          int index = View.Components.FindIndexNonAlloc(_viewComparer);
          View.Components[index].Value = (IEcsComponent)_components[i];
          View.Components[index].Name = _componentNames[i];
        }
      }

      if (!_config)
        _config = AssetFinder.FindAsset<EcsUniverseConfig>();
      if (_config)
        View.Components.Sort(_config.Comparer);

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
          _ecsNegativeComparer.Obj = View.Components[i].Name;
          if (_componentNames.AllNonAlloc(_ecsNegativeComparer))
            View.Components.RemoveAt(i--);
        }
      }

#if !DISABLE_PROFILING
      using (new ProfilerMarker("Add()").Auto())
#endif
      {
        for (int i = 0; i < _componentCount; i++)
        {
          _viewNegativeComparer.Obj = _componentNames[i];
          if (View.Components.AllNonAlloc(_viewNegativeComparer))
            View.Components.Add(new EcsComponentView
              { Value = (IEcsComponent)_components[i], Name = _componentNames[i] });
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

    private void UpdatePools()
    {
      _wrapper.World.GetAllPools(ref _pools);
    }

    public void AddComponents()
    {
      UpdatePools();
      foreach (EcsComponentView component in View.ComponentPull)
      {
        _name = EditorContext.GetPrettyName(component.Value);
        IEcsPool pool = _pools.FirstOrDefault(x => EditorContext.GetPrettyName(x.GetComponentType()) == _name) 
          ?? _wrapper.World.GetPoolEnsure(component.Value.GetType());

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
        IEcsPool pool = _pools.First(x => EditorContext.GetPrettyName(x.GetComponentType()) == _name);
        if (pool.Has(Entity))
          pool.Del(Entity);
      }
    }

    private class EcsComponentNameNegativeComparer : IPredicate<string>
    {
      public string Obj;

      public bool Predicate(string obj)
      {
        return obj != Obj;
      }
    }

    private class EcsComponentViewNegativeComparer : IPredicate<EcsComponentView>
    {
      public string Obj;

      public bool Predicate(EcsComponentView obj)
      {
        return obj.Name != Obj;
      }
    }

    private class EcsComponentViewComparer : IPredicate<EcsComponentView>
    {
      public string Obj;

      public bool Predicate(EcsComponentView obj)
      {
        return obj.Name == Obj;
      }
    }
  }
}