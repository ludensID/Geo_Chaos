using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Debugging
{
  public class EcsEntityViewDebugFactory
  {
    private readonly IInstantiator _instantiator;

    public EcsEntityViewDebugFactory(IInstantiator instantiator)
    {
      _instantiator = instantiator;
    }

    public EcsEntityViewDebug Create(EcsWorld world, int entity, Transform parent)
    {
      var instance = _instantiator.InstantiateComponentOnNewGameObject<EcsEntityViewDebug>(new[] { world });
      instance.transform.SetParent(parent);
      instance.Entity = entity;
      return instance;
    }
  }
}