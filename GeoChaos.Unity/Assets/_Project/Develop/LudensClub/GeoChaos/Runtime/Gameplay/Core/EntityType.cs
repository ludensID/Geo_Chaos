namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public enum EntityType
  {
    None = 0,
    Hero = 1,
    Enemy = Hero + 1,
    Lama = Enemy + 1,
    LeafySpirit = Lama + 1,
    Ring = 15,
    Shard = Ring + 1,
    Spike = Shard + 1,
    FadingPlatform = Spike + 1,
    Key = FadingPlatform + 1,
    Door = Key + 1,
    Lever = Door + 1,
    HealthShard = Lever + 1,
    Leaf = HealthShard + 1
  }
}