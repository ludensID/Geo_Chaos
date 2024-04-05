using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Props.Enemy
{
  public class EnemyFactory : IEnemyFactory
  {
    private readonly IInstantiator _instantiator;

    public EnemyFactory(IInstantiator instantiator)
    {
      _instantiator = instantiator;
    }

    public EnemyView Create(Vector3 position)
    {
      return _instantiator.InstantiatePrefabResourceForComponent<EnemyView>(ResourcePaths.Enemy, position,
        Quaternion.identity, null);
    }
  }
}