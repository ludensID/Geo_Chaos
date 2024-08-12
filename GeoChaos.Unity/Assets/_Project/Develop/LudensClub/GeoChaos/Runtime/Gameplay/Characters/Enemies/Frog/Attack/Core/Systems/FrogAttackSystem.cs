using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Bite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Attack.Jump;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Damage;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Attack
{
  public class FrogAttackSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _attackingFrogs;
    private readonly EcsEntities _heroes;
    private readonly FrogConfig _config;

    public FrogAttackSystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider)
    {
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<FrogConfig>();

      _heroes = _game
        .Filter<HeroTag>()
        .Collect();

      _attackingFrogs = _game
        .Filter<FrogTag>()
        .Inc<AttackCommand>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity hero in _heroes)
      foreach (EcsEntity frog in _attackingFrogs)
      {
        frog.Del<AttackCommand>()
          .Add<Attacking>();

        float heroPoint = hero.Get<ViewRef>().View.transform.position.x;
        float frogPoint = frog.Get<ViewRef>().View.transform.position.x;
        float half = _config.FrontRadius / 2;

        if (Mathf.Abs(heroPoint - frogPoint) < half)
          frog.Add<BiteCommand>();
        else
          frog.Add<AttackJumpCommand>();
      }
    }
  }
}