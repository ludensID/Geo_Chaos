﻿using TriInspector;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Configuration
{
  [CreateAssetMenu(menuName = CAC.Names.ZOMBIE_MENU, fileName = CAC.Names.ZOMBIE_FILE)]
  public class ZombieConfig : ScriptableObject
  {
    [Title("Attack")]
    public float BackDistance;

    public float AttackTime;

    [Title("Attack With Arms")]
    public float ArmsTime;

    public float ArmsCooldown;
    
    [Title("Damage")]
    public float DamageFromBody;
    public float DamageFromArms;

    [Title("Watch")]
    public float WatchTime;
  }
}