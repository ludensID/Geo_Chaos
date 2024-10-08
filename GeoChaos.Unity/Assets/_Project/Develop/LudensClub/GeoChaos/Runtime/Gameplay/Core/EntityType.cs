﻿namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public enum EntityType
  {
    None = 0,
    Hero = 1,
    Enemy = Hero + 1,
    Lama = Enemy + 1,
    LeafySpirit = Lama + 1,
    Frog = LeafySpirit + 1,
    Zombie = Frog + 1,
    Shroom = Zombie + 1,
    Ring = 200,
    Shard = Ring + 1,
    Spike = Shard + 1,
    FadingPlatform = Spike + 1,
    Key = FadingPlatform + 1,
    Door = Key + 1,
    Lever = Door + 1,
    HealthShard = Lever + 1,
    Leaf = HealthShard + 1,
    Tongue = Leaf + 1,
    GasCloud = Tongue + 1,
    SpawnPoint = 500,
    CheckPoint = SpawnPoint + 1
  }
}