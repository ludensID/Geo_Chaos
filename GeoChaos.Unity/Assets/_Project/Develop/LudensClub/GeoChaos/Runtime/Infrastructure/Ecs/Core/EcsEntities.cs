using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Leopotam.EcsLite;

namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  public class EcsEntities : IEnumerable<EcsEntity>
  {
    private readonly List<IEcsPredicate> _predicates = new List<IEcsPredicate>();
    private readonly EcsFilter _filter;
    private readonly EcsWorld _world;

    public EcsEntities(EcsFilter filter)
    {
      _filter = filter;
      _world = filter.GetWorld();
    }

    public IEnumerator<EcsEntity> GetEnumerator()
    {
      foreach (int i in _filter)
      {
        if (_predicates.All(p => p.Invoke(i)))
          yield return new EcsEntity(_world, i);
      }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }
    
    public EcsEntities Where<TComponent>(Predicate<TComponent> predicate) where TComponent : struct, IEcsComponent
    {
      var entities = new EcsEntities(_filter);
      entities._predicates.AddRange(_predicates);
      entities._predicates.Add(new EcsPredicate<TComponent>(predicate, _world.GetPool<TComponent>()));
      return entities;
    }
  }
}