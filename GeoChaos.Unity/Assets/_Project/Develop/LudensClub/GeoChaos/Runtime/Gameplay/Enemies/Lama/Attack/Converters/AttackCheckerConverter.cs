﻿using LudensClub.GeoChaos.Runtime.Constants;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemies.Lama.Attack
{
  [AddComponentMenu(ACC.Names.ATTACK_CHECKER_CONVERTER)]
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