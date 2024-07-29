using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Correction
{
  public class CorrectLeafySpiritSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _correctedSpirits;
    private readonly EcsEntities _heroes;
    private readonly LeafySpiritConfig _config;

    public CorrectLeafySpiritSystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider)
    {
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<LeafySpiritConfig>();

      _heroes = _game
        .Filter<HeroTag>()
        .Collect();

      _correctedSpirits = _game
        .Filter<LeafySpiritTag>()
        .Inc<CorrectCommand>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity hero in _heroes)
      foreach (EcsEntity spirit in _correctedSpirits)
      {
        spirit.Del<CorrectCommand>();

        Vector3 heroPosition = hero.Get<ViewRef>().View.transform.position;
        Vector3 spiritPosition = spirit.Get<ViewRef>().View.transform.position;

        float sqrDistance = (heroPosition - spiritPosition).sqrMagnitude;
        if (sqrDistance > _config.AttackDistance * _config.AttackDistance)
          spirit.Add<ShouldMoveCommand>();
        else
          spirit.Add<ShouldAttackCommand>();
      }
    }
  }
}