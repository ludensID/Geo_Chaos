using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Debugging
{
  public class EcsWorldDebugFactory
  {
    private readonly IInstantiator _instantiator;

    public EcsWorldDebugFactory(IInstantiator instantiator)
    {
      _instantiator = instantiator;
    }

    public EcsWorldDebug Create(IWorldWrapper worldWrapper)
    {
      var instance =
        _instantiator.InstantiateComponentOnNewGameObject<EcsWorldDebug>(worldWrapper.ToString(),
          new[] { worldWrapper });
      Object.DontDestroyOnLoad(instance);
      return instance;
    }
  }
}