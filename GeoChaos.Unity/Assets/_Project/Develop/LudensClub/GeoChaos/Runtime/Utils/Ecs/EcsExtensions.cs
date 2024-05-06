using System;
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

    [HideInCallstack]
    public static ref TComponent Assign<TComponent>(this ref TComponent component, Action<EcsShell<TComponent>> assigner)
      where TComponent : struct, IEcsComponent
    {
      var shell = new EcsShell<TComponent>(component);
      assigner.Invoke(shell);
      component = shell.Value;
      return ref component;
    }

    [HideInCallstack]
    public static IEnumerable<int> Where<TComponent>(this EcsFilter filter, Predicate<TComponent> predicate)
      where TComponent : struct, IEcsComponent
    {
      EcsPool<TComponent> pool = filter.GetWorld().GetPool<TComponent>();
      var selection = new List<int>();
      foreach (int i in filter)
        if (predicate.Invoke(pool.Get(i)))
          selection.Add(i);

      return selection;
    }

    [HideInCallstack]
    public static EcsEntities Collect(this EcsWorld.Mask mask)
    {
      return new EcsEntities(mask.End());
    }

    [HideInCallstack]
    public static EcsEntity CreateEntity(this EcsWorld world)
    {
      return new EcsEntity(world, world.NewEntity());
    }
    
    [HideInCallstack]
    public static bool UnpackEntity(this EcsWorld world, EcsPackedEntity packedEntity, out EcsEntity entity)
    {
      bool unpack = packedEntity.Unpack(world, out int ent);
      entity = new EcsEntity(world, ent);
      return unpack;
    }
  }
}