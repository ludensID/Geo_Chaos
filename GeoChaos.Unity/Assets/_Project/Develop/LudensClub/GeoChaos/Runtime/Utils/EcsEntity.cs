using System;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Utils
{
  public delegate void ActionRef<TComponent>(ref TComponent component);

  public class EcsEntity
  {
    public EcsWorld World { get; }
    public int Entity { get; }

    public EcsEntity(EcsWorld world, int entity)
    {
      World = world;
      Entity = entity;
    }

    [HideInCallstack]
    public EcsEntity Add<TComponent>() where TComponent : struct, IEcsComponent
    {
      World.Add<TComponent>(Entity);
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
    public ref TComponent Assign<TComponent>(ActionRef<TComponent> assigner) where TComponent : struct, IEcsComponent
    {
      ref TComponent component = ref World.Add<TComponent>(Entity);
      assigner.Invoke(ref component);
      return ref component;
    }

    [HideInCallstack]
    public ref TComponent Get<TComponent>() where TComponent : struct, IEcsComponent
    {
      return ref World.Get<TComponent>(Entity);
    }

    [HideInCallstack]
    public bool Is<TComponent>() where TComponent : struct, IEcsComponent
    {
      return World.Has<TComponent>(Entity);
    }

    [HideInCallstack]
    public ref TComponent Change<TComponent>(ActionRef<TComponent> replacer) where TComponent : struct, IEcsComponent
    {
      ref TComponent component = ref World.Get<TComponent>(Entity);
      replacer.Invoke(ref component);
      return ref component;
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
  }

  public class EcsEntity<TComponent> : EcsEntity where TComponent : struct, IEcsComponent
  {
    public EcsEntity(EcsWorld world, int entity) : base(world, entity)
    {
    }

    [HideInCallstack]
    public EcsEntity<TComponent> Add()
    {
      World.GetPool<TComponent>().Add(Entity);
      return this;
    }

    [HideInCallstack]
    public ref TComponent GetComponent()
    {
      return ref World.Get<TComponent>(Entity);
    }

    [HideInCallstack]
    public EcsEntity<TComponent> Del()
    {
      World.Del<TComponent>(Entity);
      return this;
    }

    [HideInCallstack]
    public EcsEntity<TComponent> Assign(Action<EcsShell<TComponent>> assigner)
    {
      ref TComponent ecsComponent = ref GetComponent();
      var shell = new EcsShell<TComponent>(ecsComponent);
      assigner.Invoke(shell);
      ecsComponent = shell.Value;
      return this;
    }
  }
}