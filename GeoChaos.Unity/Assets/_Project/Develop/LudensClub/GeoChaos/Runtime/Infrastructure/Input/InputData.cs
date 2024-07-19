using System;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  [Serializable]
  public class InputData
  {
    public bool IsJumpStarted;
    public bool IsJumpCanceled;
    public float HorizontalMovement;
    public float VerticalMovement;
    public bool IsDash;
    public bool IsAttack;
    public bool IsHook;
    public bool IsShoot;
    public bool IsAim;
    public Vector2 AimDirection;
    public Vector2 AimPosition;
    public Vector2 AimRotation;
    public bool IsInteraction;

    public void Clear()
    {
      IsJumpStarted = false;
      IsJumpCanceled = false;
      HorizontalMovement = 0;
      VerticalMovement = 0;
      IsDash = false;
      IsAttack = false;
      IsHook = false;
      IsShoot = false;
      IsAim = false;
      AimDirection = Vector2.zero;
      AimPosition = Vector2.zero;
      AimRotation = Vector2.zero;
      IsInteraction = false;
    }
  }
}