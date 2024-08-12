using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Jump;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.JumpCycle;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Attack.Jump
{
  public class FrogJumpAttackSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _attackingFrogs;
    private readonly EcsEntities _heroes;
    private readonly FrogConfig _config;

    public FrogJumpAttackSystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider)
    {
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<FrogConfig>();

      _heroes = _game
        .Filter<HeroTag>()
        .Collect();

      _attackingFrogs = _game
        .Filter<FrogTag>()
        .Inc<AttackJumpCommand>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity hero in _heroes)
      foreach (EcsEntity frog in _attackingFrogs)
      {
        float heroPoint = hero.Get<ViewRef>().View.transform.position.x;
        float frogPoint = frog.Get<ViewRef>().View.transform.position.x;
        float length = Mathf.Abs(heroPoint - frogPoint) - 0.1f;
        
        if (length <= 0.1f)
          length = 0;

        frog
          .Del<AttackJumpCommand>()
          .Add<JumpAttacking>()
          .Add<StartJumpCycleCommand>()
          .Replace((ref JumpPoint jumpPoint) => jumpPoint.Point = heroPoint)
          .Replace((ref FrogJumpContext ctx) =>
          {
            ctx.Length = length;
            ctx.Height = _config.JumpAttackHeight;
          });
      }
    }
  }
}