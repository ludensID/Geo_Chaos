﻿using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Utils
{
  public static class EcsUtils
  {
    public static void AssignVector(this ref MovementVector obj, Vector2 vector, bool saveXDirection = false)
    {
      (Vector2 length, Vector2 direction) = MathUtils.DecomposeVector(vector);
      obj.Speed = length;
      if (!saveXDirection || obj.Speed.x != 0)
        obj.Direction.x = direction.x;
      obj.Direction.y = direction.y;
    }
  }
}