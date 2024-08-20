using System;
using System.Reflection;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.AI;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Patrol;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Collisions;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Utils
{
  public static class EcsUtils
  {
    private static readonly MethodInfo _getPoolMethod = typeof(EcsWorld).GetMethod("GetPool");

    public static void AssignVector(this ref MovementVector obj, Vector2 vector, bool saveXDirection = false)
    {
      (Vector2 length, Vector2 direction) = MathUtils.DecomposeVector(vector);
      obj.Speed = length;
      if (!saveXDirection || obj.Speed.x != 0)
        obj.Direction.x = direction.x;
      obj.Direction.y = direction.y;
    }

    public static TContext Cast<TContext>(this BrainContext ctx) where TContext : IBrainContext
    {
      return (TContext)ctx.Context;
    }

    public static EcsEntity SetActive(this EcsEntity entity, bool value)
    {
      return entity
        .Has<Active>(value)
        .Has<Inactive>(!value);
    }

    public static Rect GetBounds(this PhysicalBoundsRef obj)
    {
      return PhysicalBoundsConverter.GetBounds(obj.Left, obj.Right);
    }

    public static bool TrySelectByColliderTypes(this ICollisionService obj, ColliderType master, ColliderType target)
    {
      return obj.TrySelectByColliders(x => x.Type == master, x => x.Type == target);
    }

    public static bool TrySelectByEntitiesTag<TMasterTag, TTargetTag>(this ICollisionService obj) where TMasterTag : struct, IEcsComponent
      where TTargetTag : struct, IEcsComponent
    {
      return obj.TrySelectByEntities(x => x.Has<TMasterTag>(), x => x.Has<TTargetTag>());
    }

    public static IEcsPool GetPoolEnsure(this EcsWorld world, Type type)
    {
      return world.GetPoolByType(type)
        ?? (IEcsPool)_getPoolMethod.MakeGenericMethod(type).Invoke(world, Array.Empty<object>());
    }
  }
}