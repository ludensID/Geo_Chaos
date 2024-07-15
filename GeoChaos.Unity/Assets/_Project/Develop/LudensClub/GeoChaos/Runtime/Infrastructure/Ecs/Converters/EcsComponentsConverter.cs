using System;
using System.Collections.Generic;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Converters
{
  [Serializable]
  public class EcsComponentsConverter : IEcsConverter
  {
    public List<EcsComponentValue> Components;

    public void Convert(EcsEntity entity)
    {
      EcsWorld world = entity.World;
      foreach (EcsComponentValue componentValue in Components)
      {
        IEcsPool pool = world.GetPoolEnsure(componentValue.Type);
        pool.AddRaw(entity.Entity, componentValue.Value);
      }
    }
  }
}