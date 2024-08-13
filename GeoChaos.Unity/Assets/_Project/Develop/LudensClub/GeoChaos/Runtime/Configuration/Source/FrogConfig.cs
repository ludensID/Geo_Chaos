using TriInspector;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Configuration
{
  [CreateAssetMenu(menuName = CAC.Names.FROG_MENU, fileName = CAC.Names.FROG_FILE)]
  public class FrogConfig : ScriptableObject
  {
    [Title("Wait")]
    public float WaitTime;

    [Title("Jump")]
    public float TimeAfterJump;

    [Title("Small Jump")]
    public float SmallJumpLength;

    public float SmallJumpHeight;

    [Title("Big Jump")]
    public float BigJumpLength;

    public float BigJumpHeight;

    [Title("Jump Back")]
    public float JumpBackLength;

    public float JumpBackHeight;

    [Title("Detection")]
    public float MaxVerticalDistance;

    public float FrontRadius;
    public float BackRadius;

    [Title("Attack")]
    public float JumpAttackHeight;

    public float BiteTime;
    public float HitCooldown;

    [Title("Watch")]
    public float WatchTime;
  }
}