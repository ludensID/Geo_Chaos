using UnityEngine;

namespace LudensClub.GeoChaos.Editor.Monitoring.Entity
{
  public interface IEcsEntityViewFactory
  {
    EcsEntityView Create(Transform parent);
  }
}