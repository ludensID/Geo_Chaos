using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Editor.Monitoring.Universe
{
  public class EcsUniverseViewFactory : IEcsUniverseViewFactory
  {
    private readonly IInstantiator _instantiator;

    public EcsUniverseViewFactory(IInstantiator instantiator)
    {
      _instantiator = instantiator;
    }

    public EcsUniverseView Create()
    {
      var instance = _instantiator.InstantiateComponentOnNewGameObject<EcsUniverseView>("[ECS-UNIVERSE]");
      instance.transform.SetParent(null);
      Object.DontDestroyOnLoad(instance);
      return instance;
    }
  }
}