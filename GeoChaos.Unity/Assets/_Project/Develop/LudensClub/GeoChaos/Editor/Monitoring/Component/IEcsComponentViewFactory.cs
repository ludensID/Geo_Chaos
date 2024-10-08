using System;
using Leopotam.EcsLite;

namespace LudensClub.GeoChaos.Editor.Monitoring.Component
{
  public interface IEcsComponentViewFactory
  {
    IEcsComponentView Create(Type componentType, int entity, IEcsPool pool);
  }
}