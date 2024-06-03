using System.Collections.Generic;
using LudensClub.GeoChaos.Runtime.Constants;
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
    [LabelText("Enabled")]
    public bool EnableDash;

    public float DashVelocity;
    public float DashTime;

    [ShowInInspector]
    [PropertyOrder(12)]
    public float DashDistance => DashVelocity * DashTime;

    public float DashCooldown;

    [Title("Attack")]
    [LabelText("Enabled")]
    public bool EnableAttack;

    [ListDrawerSettings(HideAddButton = true, HideRemoveButton = true)]
    public List<float> HitDurations = new List<float>(3) { 0, 0, 0 };

    [ListDrawerSettings(HideAddButton = true, HideRemoveButton = true)]
    public List<float> ComboAttackPeriods = new List<float>(2) { 0, 0 };

    [Title(TriConstants.Names.GRAPPLING_HOOK)]
    [LabelText("Enabled")]
    public bool EnableHook;

    public bool AllowHookInterruption;
    public float HookRadius;

    [Range(0, 90)]
    public float RingViewAngle = 90;

    [Range(1, 10)]
    [HideInInspector]
    public float RingHorizontalDistance = 1;

    [Min(0.01f)]
    public float HookPrecastTime;
    
    [PropertySpace(SpaceAfter = 20)]
    public float HookVelocity;

    [GroupNext(TriConstants.Names.HOOK_UPGRADES_TYPES), Tab("Drag Force")]
    [LabelText("Enabled")]
    public bool EnableDragForce; 
    
    [LabelText("UseGradient")]
    public bool UseDragForceGradient;
    public float StartDragForceCoefficient => 1f / (UseDragForceGradient ? 3f : 2f);
    
    public bool IsRelativeSpeed;
    
    [LabelText("Multiplier")]
    public Vector2 DragForceMultiplier;

    [GroupNext(TriConstants.Names.HOOK_UPGRADES_TYPES), Tab("AD Control")]
    [LabelText("Enabled")]
    public bool EnableADControl;

    [LabelText("UseGradient")]
    public bool UseADControlGradient;
    
    public float StartADControlCoefficient => 1f / (UseADControlGradient ? 3f : 2f);

    
    [LabelText("Speed")]
    public float ADControlSpeed;

    [GroupNext(TriConstants.TECH + TriConstants.Names.GRAPPLING_HOOK)]
    public float PullTimeOffset;

    public float HookInputCooldown;
    public float RingReleasingTime;

    [UnGroupNext]
    [Title("Shoot")]
    [LabelText("Enabled")]
    public bool EnableShoot;
    public float ShardVelocity;
    public float AutoShootRadius;
    public float ShootCooldown;
    
    [Range(0, 90)]
    public float EnemyViewAngle;

    [GroupNext(TriConstants.Names.AIM)]
    [LabelText("Enabled")]
    public bool EnableAim;
    
    public float AimRotationSpeed;

    [GroupNext(TriConstants.TECH + TriConstants.Names.SHOOT)]
    public float ShardLifeTime;
    public LayerMask ShootMask;
    
    [UnGroupNext]
    [Title("Characteristics")]
    public float Health;

    public float DashDamage;
    public float ShardDamage;

    [ListDrawerSettings(HideAddButton = true, HideRemoveButton = true)]
    public List<float> HitDamages = new List<float>(3) { 0, 0, 0 };
  }
}