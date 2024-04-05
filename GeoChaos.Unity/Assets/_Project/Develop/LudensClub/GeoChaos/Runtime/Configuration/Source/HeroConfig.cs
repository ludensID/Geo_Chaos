using TriInspector;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Configuration
{
  [CreateAssetMenu(fileName = CAC.HERO_FILE, menuName = CAC.HERO_MENU)]
  public partial class HeroConfig : ScriptableObject
  {
    [Title("Movement")]
    public float MovementSpeed;
    
    [Range(0, 1)]
    public float MovementResponseDelay;
    public float AccelerationTime;

    [Title("Jump")]
    [ShowInInspector]
    [PropertyOrder(3)]
    [Range(-50, 0)]
    public float Gravity
    {
      get => Physics2D.gravity.y * GravityScale;
      set => GravityScale = value / Physics2D.gravity.y;
    }

    [Range(0, 10)]
    public float GravityScale = 1;

    public float JumpForce;
    
    [ShowInInspector]
    [PropertyOrder(5)]
    // [Group(nameof(JumpHeight))]
    // [EnableIf(TriUtils.FREE + nameof(JumpHeight))]
    public float JumpHeight
    {
      get => -Mathf.Pow(JumpForce, 2) / (2 * Gravity);
      // set => JumpForce = Mathf.Sqrt(-2 * Gravity * value);
    }

    public float ForcedStopInertia;
    public float VelocityDropToStop;

    [ShowInInspector] 
    [PropertyOrder(7)] 
    public float VelocityToStop => JumpForce - VelocityDropToStop;

    [Title("Dash")] 
    public float DashVelocity;
    public float DashTime;
    
    [ShowInInspector]
    [PropertyOrder(9)]
    public float DashDistance => DashVelocity * DashTime;

    [Title("Characteristics")] 
    public float Health;
    public float DashDamage;
  }

  // [DeclareBoxGroup(nameof(JumpHeight), Title = "$" + nameof(TitleJumpHeight), HideTitle = true)]
  // public partial class HeroConfig
  // {
  //   [ShowInInspector]
  //   [PropertyOrder(6)]
  //   [HideLabel]
  //   [Group(nameof(JumpHeight))]
  //   private bool _freeJumpHeight;
  //
  //   private string TitleJumpHeight()
  //   {
  //     return $"{nameof(JumpHeight)}: {JumpHeight}";
  //   }
  // }
}

