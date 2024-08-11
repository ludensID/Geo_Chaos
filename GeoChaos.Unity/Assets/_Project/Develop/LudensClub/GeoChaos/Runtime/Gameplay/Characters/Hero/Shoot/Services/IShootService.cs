using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Shoot
{
  public interface IShootService
  {
    Vector2 CalculateShootDirection(Vector2 viewDirection, float bodyDirection);
  }
}