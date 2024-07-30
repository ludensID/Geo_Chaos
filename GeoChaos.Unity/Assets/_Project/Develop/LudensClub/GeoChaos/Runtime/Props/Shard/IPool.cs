using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Props.Shard
{
  public interface IPool<TView> : IPushable where TView : BaseView
  {
    TView Pop();
    TView Pop(Vector3 position, Quaternion rotation, Transform parent = null);
  }
}