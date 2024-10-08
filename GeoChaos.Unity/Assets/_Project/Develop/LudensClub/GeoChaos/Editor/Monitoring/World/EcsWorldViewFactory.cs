using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Editor.Monitoring.World
{
  public class EcsWorldViewFactory : IEcsWorldViewFactory
  {
    private readonly IInstantiator _instantiator;

    public EcsWorldViewFactory(IInstantiator instantiator)
    {
      _instantiator = instantiator;
    }

    public EcsWorldView Create(Transform parent)
    {
      var instance = _instantiator.InstantiateComponentOnNewGameObject<EcsWorldView>();
      instance.transform.SetParent(parent);
      return instance;
    }
  }
}