using System;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.AI
{
  [Serializable]
  public class LamaContext : IBrainContext
  {
    public float MovementSpeed;
    public Vector2 PatrolStep;
    public float LookingTime;
    public float ViewRadius;
    public float ListenTime;
    public float AttackDistance;
    public float HitTime;
    public float BiteTime;
    public float HitCooldown;
    public float ComboCooldown;
    public float HitDamage;
    public float BiteDamage;
  }
}