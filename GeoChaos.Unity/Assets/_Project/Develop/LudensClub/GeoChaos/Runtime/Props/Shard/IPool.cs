using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Props.Shard
{
  public interface IPool<TView> : IPushable where TView : BaseView
  {
    TView Pull();
    TView Pull(Vector3 position, Quaternion rotation, Transform parent = null);
  }
}