using System;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Utils
{
  public delegate void ActionRef<TComponent>(ref TComponent component);

  public class EcsEntity : IDisposable
  {
    public EcsWorld World { get; private set; }
    public int Entity { get; private set; }
    public bool IsAlive { get; private set; }

    public EcsEntity(EcsWorld world, int entity)
    {
      World = world;
      Entity = entity;
      IsAlive = world != null && entity != -1;
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
      ref TComponent replacedComponent = ref World.Get<TComponent>(Entity);
      replacedComponent = component;
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
      World = null;
      Entity = -1;
      IsAlive = false;
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