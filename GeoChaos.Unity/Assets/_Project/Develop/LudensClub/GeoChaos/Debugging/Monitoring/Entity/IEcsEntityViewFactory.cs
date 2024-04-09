using UnityEngine;

namespace LudensClub.GeoChaos.Debugging.Monitoring
{
  public interface IEcsEntityViewFactory
  {
    EcsEntityView Create(Transform parent);
  }
}