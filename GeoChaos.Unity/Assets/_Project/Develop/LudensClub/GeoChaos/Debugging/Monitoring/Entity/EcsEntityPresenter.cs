using System;
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

    private readonly PoolClosure _poolClosure = new PoolClosure();
    private readonly IEcsWorldWrapper _wrapper;
    private readonly IEcsEntityViewFactory _viewFactory;
    private readonly IEcsComponentViewFactory _componentFactory;
    private readonly IEcsWorldPresenter _parent;

    private EcsUniverseConfig _config;
    private int _componentCount;
    private string _name;

    public int Entity { get; }
    public string EntityString { get; }
    public EcsEntityView View { get; private set; }

    public EcsEntityPresenter(int entity,
      IEcsWorldWrapper wrapper,
      IEcsEntityViewFactory viewFactory,
      IEcsComponentViewFactory componentFactory,
      IEcsWorldPresenter parent)
    {
      _wrapper = wrapper;
      _viewFactory = viewFactory;
      _componentFactory = componentFactory;
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
      if (!View.gameObject.activeSelf)
      {
        View.Components.Clear();
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
            componentView.Name = EditorContext.GetPrettyName(componentType);
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
        _name = EditorContext.GetPrettyName(component.Value);
        IEcsPool pool = _parent.Pools.FirstOrDefault(x => EditorContext.GetPrettyName(x.GetComponentType()) == _name)
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
        _name = EditorContext.GetPrettyName(component.Value);
        IEcsPool pool = _parent.Pools.First(x => EditorContext.GetPrettyName(x.GetComponentType()) == _name);
        if (pool.Has(Entity))
          pool.Del(Entity);
      }
    }

    public void ChangeComponent(IEcsComponent component)
    {
      IEcsPool pool = _parent.Pools.First(x => x.GetComponentType() == component.GetType());
      pool.SetRaw(Entity, component);
    }

    private class PoolClosure : EcsClosure<IEcsComponentView>
    {
      public IEcsPool Pool;

      public Predicate<IEcsComponentView> SpecifyPredicate(IEcsPool pool)
      {
        Pool = pool;
        return Predicate;
      }

      protected override bool Call(IEcsComponentView value)
      {
        return value.Pool == Pool;
      }
    }
  }
}