using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Creation;
using LudensClub.GeoChaos.Runtime.Gameplay.Environment.Checkpoint;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Persistence;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero
{
  public class LoadHeroSystem : IEcsRunSystem
  {
    private readonly IPersistenceService _persistence;
    private readonly EcsWorld _game;
    private readonly EcsEntities _checkpoints;
    private readonly EcsEntities _heroes;

    public LoadHeroSystem(GameWorldWrapper gameWorldWrapper, IPersistenceService persistence)
    {
      _persistence = persistence;
      _game = gameWorldWrapper.World;

      _checkpoints = _game
        .Filter<CheckpointTag>()
        .Collect();

      _heroes = _game
        .Filter<HeroTag>()
        .Inc<OnConverted>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity checkpoint in _checkpoints)
      {
        Vector2 position = checkpoint.Get<ViewRef>().View.transform.position;
        if (position == (Vector2)_persistence.Data.LastCheckpoint)
        {
          foreach (EcsEntity hero in _heroes)
          {
            hero.Get<ViewRef>().View.transform.position = position;
          }
        }
      }
    }
  }
}