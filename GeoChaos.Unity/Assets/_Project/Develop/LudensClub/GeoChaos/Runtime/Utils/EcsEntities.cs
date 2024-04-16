using System;
using System.Collections;
using System.Collections.Generic;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Utils
{
  public class EcsEntities : IEnumerable<EcsEntity>
  {
    public EcsFilter Filter { get; }
    public EcsWorld World { get; }

    public EcsEntities(EcsFilter filter)
    {
      Filter = filter;
      World = filter.GetWorld();
    }

    public IEnumerator<EcsEntity> GetEnumerator()
    {
      var list = new List<EcsEntity>();
      foreach (int i in Filter)
      {
        list.Add(new EcsEntity(World, i));
      }

      return list.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    public IEnumerable<EcsEntity> Where<TComponent>(Predicate<TComponent> predicate)
      where TComponent : struct, IEcsComponent
    {
      EcsPool<TComponent> pool = World.GetPool<TComponent>();
      var selection = new List<EcsEntity>();
      foreach (int i in Filter)
      {
        if (predicate.Invoke(pool.Get(i)))
          selection.Add(new EcsEntity(World, i));
      }

      return selection;
    }
  }
}