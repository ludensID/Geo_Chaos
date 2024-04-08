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

    [ShowInInspector]
    [Group(TriConstants.TECH + TriConstants.Names.JUMP)]
    public float GravityScale => Gravity / Physics2D.gravity.y;

    [ShowInInspector]
    [Group(TriConstants.TECH + TriConstants.Names.JUMP)]
    public float FallGravityScale => Mathf.Pow(FallVelocityMultiplier, 2) * GravityScale;

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
    [PropertyOrder(10)]
    public float DashDistance => DashVelocity * DashTime;

    [Title("Characteristics")]
    public float Health;

    public float DashDamage;
  }

#if UNITY_EDITOR
  [DeclareFoldoutGroup(TriConstants.TECH + TriConstants.Names.JUMP, Title = TriConstants.TECH)]
  public partial class HeroConfig
  {
    public void OnJumpHorizontalSpeedMultiplierChanged()
    {
      JumpLength = MovementSpeed * JumpHorizontalSpeedMultiplier * JumpTime;
    }
  }
#endif
}