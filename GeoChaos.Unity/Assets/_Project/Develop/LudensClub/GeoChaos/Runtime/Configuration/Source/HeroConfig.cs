using System.Collections.Generic;
using LudensClub.GeoChaos.Runtime.Utils;
using TriInspector;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Configuration
{
  [CreateAssetMenu(fileName = CAC.HERO_FILE, menuName = CAC.HERO_MENU)]
  public partial class HeroConfig : ScriptableObject
  {
    [Range(0, 1)]
    public float MovementResponseDelay;

    [Title("Movement")]
    public float MovementSpeed;

    public float AccelerationTime;

    [Title(TriConstants.Names.JUMP)]
    public float JumpTime;

    public float JumpHeight;

    [Range(0.001f, 10)]
    public float FallVelocityMultiplier = 1;

    [OnValueChanged(TriConstants.ON + nameof(JumpHorizontalSpeedMultiplier) + TriConstants.CHANGED)]
    [Range(0.001f, 10)]
    public float JumpHorizontalSpeedMultiplier = 1;

    [ReadOnly]
    public float JumpLength;

    [ShowInInspector]
    [PropertyOrder(8)]
    [Group(TriConstants.TECH + TriConstants.Names.JUMP)]
    public float Gravity => -2 * JumpHeight * Mathf.Pow(1 + 1 / FallVelocityMultiplier, 2) / (JumpTime * JumpTime);

    public float PositiveGravity => Mathf.Abs(Gravity);

    [ShowInInspector]
    [Group(TriConstants.TECH + TriConstants.Names.JUMP)]
    public float GravityScale => Gravity / Physics2D.gravity.y;

    [ShowInInspector]
    [Group(TriConstants.TECH + TriConstants.Names.JUMP)]
    public float FallGravityScale => Mathf.Pow(FallVelocityMultiplier, 2) * GravityScale;

    public float FallGravity => Physics.gravity.y * FallGravityScale;

    public float PositiveFallGravity => Mathf.Abs(FallGravity);

    [ShowInInspector]
    [Group(TriConstants.TECH + TriConstants.Names.JUMP)]
    public float JumpForce => (1 + 1 / FallVelocityMultiplier) * 2 * JumpHeight / JumpTime;

    [ShowInInspector]
    [Group(TriConstants.TECH + TriConstants.Names.JUMP)]
    public float JumpHorizontalSpeed => MovementSpeed * JumpHorizontalSpeedMultiplier;

    [Title("Dash")]
    public float DashVelocity;

    public float DashTime;

    [ShowInInspector]
    [PropertyOrder(11)]
    public float DashDistance => DashVelocity * DashTime;

    public float DashCooldown;

    [Title("Attack")]
    [ListDrawerSettings(HideAddButton = true, HideRemoveButton = true)]
    public List<float> HitDurations = new(3) { 0, 0, 0 };

    [ListDrawerSettings(HideAddButton = true, HideRemoveButton = true)]
    public List<float> ComboAttackPeriods = new(2) { 0, 0 };

    [Title(TriConstants.Names.GRAPPLING_HOOK)]
    public bool AllowHookInterruption;
    public float HookRadius;

    [Range(1, 10)]
    public float RingHorizontalDistance = 1;

    [Min(0.01f)]
    public float HookPrecastTime;
    public float HookVelocity;
    
    [GroupNext(TriConstants.TECH + TriConstants.Names.GRAPPLING_HOOK)]
    public float PullUpHeight;
    public float PullTimeOffset;
    public float HookInputCooldown;
    public float RingReleasingTime;

    [UnGroupNext]
    [Title("Characteristics")]
    public float Health;

    public float DashDamage;

    [ListDrawerSettings(HideAddButton = true, HideRemoveButton = true)]
    public List<float> HitDamages = new(3) { 0, 0, 0 };
  }
}