using TriInspector;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Configuration
{
  [CreateAssetMenu(fileName = CAC.HERO_FILE, menuName = CAC.HERO_MENU)]
  public class HeroConfig : ScriptableObject
  {
    public float MovementSpeed;
    [Range(0, 1)] public float MovementResponseDelay;
    public float AccelerationTime;

    // [Range(-50, 0)] public float Gravity;
    // [OnValueChanged(nameof(OnGravityScaleChanged))]
    // [Range(0, 3)]
    // public float GravityScale = 1;

// #if UNITY_EDITOR
//     [Button]
//     [PropertyOrder(4)]
//     public void ToDefaultGravity()
//     {
//       Gravity = Physics2D.gravity.y;
//     }
// #endif

    // [OnValueChanged(nameof(OnJumpCoefficientChanged))]
    // [Range(0, 3)]
    // public float JumpCoefficient = 1;

    // [OnValueChanged(nameof(OnJumpForceChanged))]
    public float JumpForce;

    public float ForcedStopInertia;
    public float VelocityDropToStop;

    [ShowInInspector] public float VelocityToStop => JumpForce - VelocityDropToStop;

// #if UNITY_EDITOR
//     public void OnJumpCoefficientChanged()
//     {
//       JumpForce = JumpCoefficient * Mathf.Abs(Gravity);
//     }
//
//     public void OnJumpForceChanged()
//     {
//       JumpCoefficient = JumpForce / Mathf.Abs(Gravity);
//     }
//
//     public void OnGravityScaleChanged()
//     {
//       
//     }
// #endif
  }
}