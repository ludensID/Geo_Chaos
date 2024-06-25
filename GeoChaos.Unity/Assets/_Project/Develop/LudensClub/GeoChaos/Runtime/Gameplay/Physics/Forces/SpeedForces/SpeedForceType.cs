namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces
{
  public enum SpeedForceType
  {
    Move = 1,
    Jump = 2,
    Dash = 3,
    Hook = 4,
    Attack = 6,
    Chase = Attack + 1,
    Sneak = Chase + 1
  }
}