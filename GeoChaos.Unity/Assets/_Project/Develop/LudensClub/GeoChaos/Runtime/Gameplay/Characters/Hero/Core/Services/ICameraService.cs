﻿using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero
{
  public interface ICameraService
  {
    Vector3 ScreenToWorldPoint(Vector3 position);
    Vector3 WorldToScreenPoint(Vector3 position);
  }
}