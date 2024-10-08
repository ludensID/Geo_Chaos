using UnityEngine;

namespace LudensClub.GeoChaos.Editor.Monitoring.World
{
  public interface IEcsWorldViewFactory
  {
    EcsWorldView Create(Transform parent);
  }
}