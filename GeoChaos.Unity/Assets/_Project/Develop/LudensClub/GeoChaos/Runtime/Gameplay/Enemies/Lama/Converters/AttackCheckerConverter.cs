﻿using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemies.Lama
{
  public class AttackCheckerConverter : MonoBehaviour, IEcsConverter
  {
    [SerializeField]
    private Transform AttackChecker;
    
    public void Convert(EcsEntity entity)
    {
      entity.Add((ref AttackCheckerRef checker) => checker.AttackChecker = AttackChecker);
    }
  }
}