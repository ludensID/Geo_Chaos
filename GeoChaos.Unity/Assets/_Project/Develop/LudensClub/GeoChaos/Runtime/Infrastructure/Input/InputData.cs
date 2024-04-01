using System;

namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  [Serializable]
  public struct InputData
  {
    public bool IsJumpStarted;
    public bool IsJumpCanceled;
    public float HorizontalMovement;
  }
}