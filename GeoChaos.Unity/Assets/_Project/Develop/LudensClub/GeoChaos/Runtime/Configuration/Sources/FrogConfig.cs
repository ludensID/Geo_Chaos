using TriInspector;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Configuration
{
  [CreateAssetMenu(menuName = CAC.Names.FROG_MENU, fileName = CAC.Names.FROG_FILE)]
  public class FrogConfig : ScriptableObject
  {
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
    public float FrontRadius;

    public float BackRadius;

    [Title("Attack")]
    public float JumpAttackHeight;

    public float BiteTime;
    public float HitCooldown;

    public float DamageFromBody;
    public float DamageFromBite;
    public float DamageFromJump;
    public float DamageFromTongue;

    [Title("Watch")]
    public float WatchTime;

    [Title("Turn")]
    public float TimeBeforeTurn;

    [Title("Stun")]
    public float StunTime;

    [Title("Bump")]
    public Vector2 BumpForce;
  }
}