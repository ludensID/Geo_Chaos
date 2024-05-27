using System;

namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  [Serializable]
  public struct InputData
  {
    public bool IsJumpStarted;
    public bool IsJumpCanceled;
    public float HorizontalMovement;
    public float VerticalMovement;
    public bool IsDash;
    public bool IsAttack;
    public bool IsHook;
    public bool IsShoot;
  }
}