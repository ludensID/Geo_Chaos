using System;
using Leopotam.EcsLite;

namespace LudensClub.GeoChaos.Debugging.Monitoring
{
  public interface IEcsComponentViewFactory
  {
    IEcsComponentView Create(Type componentType, int entity, IEcsPool pool);
  }
}