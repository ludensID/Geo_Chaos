﻿using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Creation.Components;
using LudensClub.GeoChaos.Runtime.Gameplay.Enemy;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Enemy
{
  public class DeleteInitializeCommandForEnemySystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsFilter _enemies;

    public DeleteInitializeCommandForEnemySystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _enemies = _game
        .Filter<InitializeCommand>()
        .Inc<EnemyTag>()
        .End();
    }

    public void Run(EcsSystems systems)
    {
      foreach (var enemy in _enemies) _game.Del<InitializeCommand>(enemy);
    }
  }
}