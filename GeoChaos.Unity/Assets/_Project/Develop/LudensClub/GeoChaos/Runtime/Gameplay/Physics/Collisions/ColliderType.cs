namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Collisions
{
  public enum ColliderType
  {
    None = 0,
    Body = 1,
    Attack = 2,
    Dash = 100,
    Shard = Dash + 1,
    Action = Shard + 1
  }
}