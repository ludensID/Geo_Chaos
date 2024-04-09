using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Debugging.Monitoring
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
      Object.DontDestroyOnLoad(instance);
      return instance;
    }
  }
}