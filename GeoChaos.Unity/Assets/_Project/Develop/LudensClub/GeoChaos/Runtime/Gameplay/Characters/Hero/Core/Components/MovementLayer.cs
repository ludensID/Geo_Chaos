using System;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero
{
  [Flags]
  public enum MovementLayer
  {
    None = 0,
    Shoot = 1,
    Stay = (1 << 1) | Shoot,
    Interrupt = 1 << 2,
    All = int.MaxValue
  }
}