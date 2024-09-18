using System.Collections.Generic;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Hook;
using TriInspector;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Configuration
{
  [CreateAssetMenu(fileName = CAC.Names.HERO_FILE, menuName = CAC.Names.HERO_MENU)]
  public partial class HeroConfig : ScriptableObject
  {
    [PropertySpace(SpaceAfter = 20)]
    [Range(0, 1)]
    public float MovementResponseDelay;
    
    [GroupNext(TriConstants.Names.Explicit.FREE_FALLING_TABS), Tab("Drag Force")]
    [LabelText("Enabled")]
    public bool EnableDragForce; 
    
    [LabelText("UseGradient")]
    public bool UseDragForceGradient;
    public float StartDragForceCoefficient => 1f / (UseDragForceGradient ? 3f : 2f);
    
    public bool IsRelativeSpeed;
    
    [LabelText("Multiplier")]
    public Vector2 DragForceMultiplier;

    [GroupNext(TriConstants.Names.Explicit.FREE_FALLING_TABS), Tab("AD Control")]
    [LabelText("Enabled")]
    public bool EnableADControl;

    [LabelText("UseGradient")]
    public bool UseADControlGradient;
    
    public float StartADControlCoefficient => 1f / (UseADControlGradient ? 3f : 2f);
    
    [LabelText("Speed")]
    public float ADControlSpeed;

    [UnGroupNext]
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

    public float Gravity => -2 * JumpHeight * Mathf.Pow(1 + 1 / FallVelocityMultiplier, 2) / (JumpTime * JumpTime);
    
    public float GravityScale => Gravity / Physics2D.gravity.y;

    public float FallGravityScale => Mathf.Pow(FallVelocityMultiplier, 2) * GravityScale;

    public float JumpForce => (1 + 1 / FallVelocityMultiplier) * 2 * JumpHeight / JumpTime;

    public float JumpHorizontalSpeed => MovementSpeed * JumpHorizontalSpeedMultiplier;

    public float PositiveGravity => Mathf.Abs(Gravity);

    public float FallGravity => Physics.gravity.y * FallGravityScale;

    public float PositiveFallGravity => Mathf.Abs(FallGravity);
      
    [Title("Dash")]
    [LabelText("Enabled")]
    public bool EnableDash;

    public float DashVelocity;
    public float DashTime;

    [ShowInInspector]
    [PropertyOrder(20)]
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

    [Min(0.01f)]
    public float HookPrecastTime;
    
    public float HookVelocity;

    [EnumToggleButtons]
    public BumpOnHookReactionType BumpOnHookReaction;

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

    public bool EnableAim;

    [GroupNext(TriConstants.TECH + TriConstants.Names.SHOOT)]
    public float ShardLifeTime;
    public LayerMask ShootMask;

    [UnGroupNext]
    [Title("Immunity")]
    public float ImmunityTime;
    
    [Title("Bump")]
    [LabelText("Enabled")]
    public bool EnableBump;

    public Vector2 BumpForce;
    public float BumpFreezeDuration;
    
    [UnGroupNext]
    [Title("Characteristics")]
    public float HealthShardPoint;

    public float DashDamage;
    public float ShardDamage;

    [ListDrawerSettings(HideAddButton = true, HideRemoveButton = true)]
    public List<float> HitDamages = new List<float>(3) { 0, 0, 0 };
  }
}