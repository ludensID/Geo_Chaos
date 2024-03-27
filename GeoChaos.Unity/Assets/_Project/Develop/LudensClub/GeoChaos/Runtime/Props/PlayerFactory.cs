using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Props
{
  public class PlayerFactory : IPlayerFactory
  {
    private readonly IInstantiator _instantiator;

    public PlayerFactory(IInstantiator instantiator)
    {
      _instantiator = instantiator;
    }

    public PlayerView Create(Vector3 position)
    {
      return _instantiator.InstantiatePrefabResourceForComponent<PlayerView>(ResourcePaths.Player, position,
        Quaternion.identity, null);
    }
  }
}