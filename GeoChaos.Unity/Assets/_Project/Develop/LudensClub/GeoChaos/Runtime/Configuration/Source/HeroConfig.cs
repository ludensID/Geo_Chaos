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
    public float FallGravityMultiplier;
    // public float JumpLength;

    [ShowInInspector]
    [PropertyOrder(6)]
    [Group(TriConstants.TECH + TriConstants.Names.JUMP)]
    public float Gravity => -2 * JumpHeight * Mathf.Pow(1 + 1 / FallGravityMultiplier, 2) / (JumpTime * JumpTime);

    [ShowInInspector]
    [Group(TriConstants.TECH + TriConstants.Names.JUMP)]
    public float GravityScale => Gravity / Physics2D.gravity.y;

    [ShowInInspector]
    [Group(TriConstants.TECH + TriConstants.Names.JUMP)]
    public float FallGravityScale => Mathf.Pow(FallGravityMultiplier, 2) * GravityScale;

    [ShowInInspector]
    [Group(TriConstants.TECH + TriConstants.Names.JUMP)]
    public float JumpForce => (1 + 1 / FallGravityMultiplier) * 2 * JumpHeight / JumpTime;

    // [ShowInInspector]
    // [Group(TriConstants.TECH + TriConstants.Names.JUMP)]
    // public float JumpHorizontalSpeedMultiplier => JumpLength / (MovementSpeed * JumpTime);
    //
    // [ShowInInspector]
    // [Group(TriConstants.TECH + TriConstants.Names.JUMP)]
    // public float JumpHorizontalSpeed => JumpLength / JumpTime;

    [Title("Dash")]
    public float DashVelocity;

    public float DashTime;

    [ShowInInspector]
    [PropertyOrder(8)]
    public float DashDistance => DashVelocity * DashTime;

    [Title("Characteristics")]
    public float Health;

    public float DashDamage;
  }

  [DeclareFoldoutGroup(TriConstants.TECH + TriConstants.Names.JUMP, Title = TriConstants.TECH)]
  public partial class HeroConfig
  {
  }
}