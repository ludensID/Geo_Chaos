using System;
using Leopotam.EcsLite;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  public delegate void ActionRef<TComponent>(ref TComponent component);

  public class EcsEntity : IDisposable
  {
    private int _entity;
    private EcsWorld _world;

    public EcsWorld World
    {
      get => _world;
      set => _world = value;
    }

    public int Entity
    {
      get => _entity;
      set
      {
        if (_entity == value)
          return;

        _entity = value;

        if (_world != null && _entity != -1)
          PackedEntity = _world.PackEntity(value);
      }
    }

    public EcsPackedEntity PackedEntity { get; private set; }

    public bool IsAlive => _world != null && PackedEntity.Unpack(_world, out _entity) && _entity != -1;

    public EcsEntity()
    {
      _entity = -1;
    }

    public EcsEntity(EcsWorld world, int entity)
    {
      SetWorld(world, entity);
    }

    public void SetWorld(EcsWorld world, int entity)
    {
      World = world;
      Entity = entity;
    }

    public EcsEntity Clone()
    {
      return new EcsEntity(_world, _entity);
    }

    [HideInCallstack]
    public EcsEntity Add<TComponent>() where TComponent : struct, IEcsComponent
    {
      World.Add<TComponent>(Entity);
      return this;
    }

    [HideInCallstack]
    public EcsEntity Add<TComponent>(TComponent component) where TComponent : struct, IEcsComponent
    {
      ref TComponent addedComponent = ref World.Add<TComponent>(Entity);
      addedComponent = component;
      return this;
    }

    [HideInCallstack]
    public EcsEntity Add<TComponent>(ActionRef<TComponent> assigner) where TComponent : struct, IEcsComponent
    {
      ref TComponent component = ref World.Add<TComponent>(Entity);
      assigner.Invoke(ref component);
      return this;
    }

    [HideInCallstack]
    public ref TComponent Get<TComponent>() where TComponent : struct, IEcsComponent
    {
      return ref World.Get<TComponent>(Entity);
    }

    [HideInCallstack]
    public bool Has<TComponent>() where TComponent : struct, IEcsComponent
    {
      return World.Has<TComponent>(Entity);
    }

    [HideInCallstack]
    public EcsEntity Has<TComponent>(bool value) where TComponent : struct, IEcsComponent
    {
      switch (value, World.Has<TComponent>(Entity))
      {
        case (true, false):
          World.Add<TComponent>(Entity);
          break;
        case (false, true):
          World.Del<TComponent>(Entity);
          break;
      }

      return this;
    }

    [HideInCallstack]
    public EcsEntity Replace<TComponent>(TComponent component) where TComponent : struct, IEcsComponent
    {
      ref TComponent refComponent = ref World.Get<TComponent>(Entity);
      refComponent = component;
      return this;
    }

    [HideInCallstack]
    public EcsEntity Replace<TComponent>(ActionRef<TComponent> replacer) where TComponent : struct, IEcsComponent
    {
      ref TComponent component = ref World.Get<TComponent>(Entity);
      replacer.Invoke(ref component);
      return this;
    }

    [HideInCallstack]
    public EcsEntity Del<TComponent>() where TComponent : struct, IEcsComponent
    {
      World.Del<TComponent>(Entity);
      return this;
    }

    public void Dispose()
    {
      World.DelEntity(Entity);
      Entity = -1;
    }

    public EcsPackedEntity Pack()
    {
      return World.PackEntity(Entity);
    }

    public EcsPackedEntityWithWorld PackWithWorld()
    {
      return World.PackEntityWithWorld(Entity);
    }
  }
}