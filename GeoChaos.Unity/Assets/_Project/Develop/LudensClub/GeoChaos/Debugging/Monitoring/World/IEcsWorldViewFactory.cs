using UnityEngine;

namespace LudensClub.GeoChaos.Debugging.Monitoring
{
  public interface IEcsWorldViewFactory
  {
    EcsWorldView Create(Transform parent);
  }
}