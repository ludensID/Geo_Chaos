using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero
{
  public class ShootService : IShootService
  {
    public Vector2 CalculateShootDirection(Vector2 viewDirection, float bodyDirection)
    {
      Vector2 shootDirection = viewDirection;
      shootDirection.y = MathUtils.Clamp(shootDirection.y, 0);

      if (shootDirection == Vector2.zero)
        shootDirection = Vector2.right * bodyDirection;

      shootDirection.Normalize();
      return shootDirection;
    }
  }
}