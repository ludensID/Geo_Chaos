using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  public interface IPool<out TView> : IPushable where TView : BaseEntityView
  {
    TView Pop();
    TView Pop(Vector3 position, Quaternion rotation, Transform parent = null);
  }
}