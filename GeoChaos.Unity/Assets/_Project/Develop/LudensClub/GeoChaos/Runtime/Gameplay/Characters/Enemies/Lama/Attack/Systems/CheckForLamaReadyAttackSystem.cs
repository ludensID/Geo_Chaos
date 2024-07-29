using System.Collections.Generic;
using System.Linq;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.View;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Lama.Attack
{
  public class CheckForLamaReadyAttackSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _lamas;
    private readonly EcsEntities _heroes;
    private readonly LamaConfig _config;
    private readonly ContactFilter2D _filter;
    private readonly List<RaycastHit2D> _hits;

    public CheckForLamaReadyAttackSystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider)
    {
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<LamaConfig>();

      _lamas = _game
        .Filter<LamaTag>()
        .Inc<ViewRef>()
        .Collect();

      _heroes = _game
        .Filter<HeroTag>()
        .Inc<ViewRef>()
        .Collect();
      
      _hits = new List<RaycastHit2D>(10);
      _filter = new ContactFilter2D { useTriggers = false };
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity hero in _heroes)
      {
        Collider2D collider = hero.Get<ColliderRef>().Collider;
        foreach (EcsEntity lama in _lamas)
        {
          Vector3 origin = lama.Get<AttackCheckerRef>().AttackChecker.position;
          float bodyDirection = lama.Get<BodyDirection>().Direction;
          Vector3 direction = Vector3.right * bodyDirection;

          _hits.Clear();
          Physics2D.Raycast(origin, direction, _filter, _hits, _config.AttackDistance);
          lama.Has<ReadyAttack>(_hits.Any(x => x.collider == collider));
        }
      }
    }
  }
}