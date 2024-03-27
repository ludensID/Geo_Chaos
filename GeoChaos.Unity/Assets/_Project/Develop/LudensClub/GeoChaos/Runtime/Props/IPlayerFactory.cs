using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Props
{
  public interface IPlayerFactory
  {
    PlayerView Create(Vector3 position);
  }
}