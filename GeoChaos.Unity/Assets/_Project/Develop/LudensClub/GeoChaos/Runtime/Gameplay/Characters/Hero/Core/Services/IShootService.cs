using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero
{
  public interface IShootService
  {
    Vector2 CalculateShootDirection(Vector2 viewDirection, float bodyDirection);
  }
}