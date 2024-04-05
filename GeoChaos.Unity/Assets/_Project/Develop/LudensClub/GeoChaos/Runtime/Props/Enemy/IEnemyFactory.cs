using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Props.Enemy
{
  public interface IEnemyFactory
  {
    EnemyView Create(Vector3 position);
  }
}