using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Props
{
  public class HeroFactory : IHeroFactory
  {
    private readonly IInstantiator _instantiator;

    public HeroFactory(IInstantiator instantiator)
    {
      _instantiator = instantiator;
    }

    public HeroView Create(Vector3 position)
    {
      return _instantiator.InstantiatePrefabResourceForComponent<HeroView>(ResourcePaths.Player, position,
        Quaternion.identity, null);
    }
  }
}