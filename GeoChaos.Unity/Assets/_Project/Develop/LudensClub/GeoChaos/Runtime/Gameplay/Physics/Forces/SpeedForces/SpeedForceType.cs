namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces
{
  public enum SpeedForceType
  {
    Move = 1,
    Jump = 2,
    Dash = 3,
    Hook = 4,
    Attack = Hook + 1,
    Bump = Attack + 1,
    Chase = Bump + 1,
    Sneak = Chase + 1
  }
}