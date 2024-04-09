using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Debugging.Monitoring
{
  public class EcsEntityViewFactory : IEcsEntityViewFactory
  {
    private readonly IInstantiator _instantiator;

    public EcsEntityViewFactory(IInstantiator instantiator)
    {
      _instantiator = instantiator;
    }

    public EcsEntityView Create(Transform parent)
    {
      var instance = _instantiator.InstantiateComponentOnNewGameObject<EcsEntityView>();
      instance.transform.SetParent(parent);
      return instance;
    }
  }
}