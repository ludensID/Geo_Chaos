using System;
using System.Collections;
using System.Collections.Generic;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Utils
{
  public static class EcsExtensions
  {
    [HideInCallstack]
    public static ref TComponent Add<TComponent>(this EcsWorld world, int entity)
      where TComponent : struct, IEcsComponent
    {
      return ref world.GetPool<TComponent>().Add(entity);
    }

    [HideInCallstack]
    public static ref TComponent Get<TComponent>(this EcsWorld world, int entity)
      where TComponent : struct, IEcsComponent
    {
      return ref world.GetPool<TComponent>().Get(entity);
    }

    [HideInCallstack]
    public static bool Has<TComponent>(this EcsWorld world, int entity) where TComponent : struct, IEcsComponent
    {
      return world.GetPool<TComponent>().Has(entity);
    }

    [HideInCallstack]
    public static void Del<TComponent>(this EcsWorld world, int entity) where TComponent : struct, IEcsComponent
    {
      world.GetPool<TComponent>().Del(entity);
    }

    public delegate void ActionRef<T>(ref T arg1) where T : struct;

    [HideInCallstack]
    public static ref TComponent Assign<TComponent>(this ref TComponent component,
      ActionRef<TComponent> assigner) where TComponent : struct, IEcsComponent
    {
      assigner.Invoke(ref component);
      return ref component;
    }

    public delegate bool PredicateRef<T>(ref T arg1) where T : struct;

    [HideInCallstack]
    public static IEnumerable<int> Where<TComponent>(this EcsFilter filter, PredicateRef<TComponent> predicate)
      where TComponent : struct, IEcsComponent
    {
      var pool = filter.GetWorld().GetPool<TComponent>();
      var selection = new List<int>();
      foreach (var i in filter)
        if (predicate.Invoke(ref pool.Get(i)))
          selection.Add(i);

      return selection;
    }

    public static ref ViewRef CreateView(this EcsWorld world, int entity, Func<View> creator)
    {
      ref var viewRef = ref world.Add<ViewRef>(entity);
      viewRef.View = creator.Invoke();
      viewRef.View.Entity = world.PackEntity(entity);
      return ref viewRef;
    }
  }
}