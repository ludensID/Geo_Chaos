using System;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Utils
{
  public static class EcsExtensions
  {
    public static ref ViewRef CreateView(this EcsWorld obj, int entity, Func<View> creator)
    {
      ref ViewRef viewRef = ref obj.GetPool<ViewRef>().Add(entity);
      viewRef.Value = creator.Invoke();
      viewRef.Value.Entity = obj.PackEntityWithWorld(entity);
      return ref viewRef;
    }
  }
}