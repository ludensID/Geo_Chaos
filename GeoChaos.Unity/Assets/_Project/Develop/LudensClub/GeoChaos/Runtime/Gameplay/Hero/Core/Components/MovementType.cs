namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero
{
  public enum MovementType
  {
    None = 0,
    Move = 1,
    Jump = Move + 1,
    Dash = Jump + 1,
    Hook = Dash + 1,
    Attack = Hook + 1,
    Aim = Attack + 1,
    Bump = Aim + 1
  }
}