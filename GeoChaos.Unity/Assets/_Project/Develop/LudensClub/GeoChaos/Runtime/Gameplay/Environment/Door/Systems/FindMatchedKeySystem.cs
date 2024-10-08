﻿using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Interaction;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.Door
{
  public class FindMatchedKeySystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _interactedDoors;

    public FindMatchedKeySystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _interactedDoors = _game
        .Filter<DoorTag>()
        .Inc<OnInteracted>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity door in _interactedDoors)
      {
        BaseEntityView keyView = door.Get<MatchedKeyRef>().Key;
        if (keyView && keyView.Entity.TryUnpackEntity(_game, out EcsEntity key)
          && key.Has<Owner>()
          && key.Get<Owner>().Entity.TryUnpackEntity(_game, out EcsEntity hero)
          && hero.Has<HeroTag>())
        {
          door.Add<OpenCommand>();
        }
      }
    }
  }
}
