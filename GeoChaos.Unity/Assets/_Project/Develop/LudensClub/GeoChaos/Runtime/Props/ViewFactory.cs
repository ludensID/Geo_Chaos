using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Props
{
  public class ViewFactory : IViewFactory
  {
    private readonly IInstantiator _instantiator;
    private readonly PrefabConfig _prefabs;

    public ViewFactory(IInstantiator instantiator, IConfigProvider configProvider)
    {
      _instantiator = instantiator;
      _prefabs = configProvider.Get<PrefabConfig>();
    }
    
    public TComponent Create<TComponent>(EntityType id) where TComponent : Component
    {
      return _instantiator.InstantiatePrefabForComponent<TComponent>(_prefabs.Get(id));
    }
    
    public TComponent Create<TComponent>(Component prefab) where TComponent : Component
    {
      return _instantiator.InstantiatePrefabForComponent<TComponent>(prefab);
    }
  }
}