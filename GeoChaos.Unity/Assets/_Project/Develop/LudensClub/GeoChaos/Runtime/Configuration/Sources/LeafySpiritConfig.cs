﻿using System.Collections.Generic;
using TriInspector;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Configuration
{
  [CreateAssetMenu(menuName = CAC.Names.LEAFY_SPIRIT_MENU, fileName = CAC.Names.LEAFY_SPIRIT_FILE)]
  public class LeafySpiritConfig : ScriptableObject
  {
    [Title("Leap")]
    public float PrecastLeapTime;

    public float LeapTime;
    public float MinLeapDistance;

    [Title("Rise")]
    public float RisingTime;

    [Title("Correction")]
    public float AttackDistance;

    [Title("Movement")]
    public float Speed;

    public float AttackDistanceMultiplier = 1;

    [Title("Attack")]
    [OnValueChanged(TriConstants.ON + nameof(NumberOfLeaves) + TriConstants.CHANGED)]
    public int NumberOfLeaves;
    
    [ListDrawerSettings(HideRemoveButton = true, HideAddButton = true)]
    public List<float> Cooldowns = new List<float>();
    
    public float DamageFromLeaf;
    public float DamageFromBody;

    [Title("Wait After Attack")]
    public float WaitAfterAttackTime;

    [Title("Watching")]
    public float WatchingTime;

    [Title("Relaxation")]
    public float RelaxationTime;

    public float DampingDamageByDistance;

#if UNITY_EDITOR
    private void Reset()
    {
      Cooldowns = new List<float>(NumberOfLeaves);
    }

    private void OnNumberOfLeavesChanged()
    {
      int cooldownCount = NumberOfLeaves - 1;
      if (Cooldowns.Count != cooldownCount)
      {
        if (cooldownCount <= 0)
        {
          Cooldowns.Clear();
          return;
        }

        var cooldowns = new List<float>(cooldownCount);
        for (int i = 0; i < cooldownCount; i++)
          cooldowns.Add(i < Cooldowns.Count ? Cooldowns[i] : 0);

        Cooldowns.Clear();
        Cooldowns.AddRange(cooldowns);
      }
    }
#endif
  }
}