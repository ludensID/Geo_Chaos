using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Chase;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Jump;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.JumpCycle;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Chase
{
  public class FrogChasingSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _chasingFrogs;
    private readonly EcsEntities _heroes;
    private readonly FrogConfig _config;

    public FrogChasingSystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider)
    {
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<FrogConfig>();

      _heroes = _game
        .Filter<HeroTag>()
        .Collect();

      _chasingFrogs = _game
        .Filter<FrogTag>()
        .Inc<ChaseCommand>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity hero in _heroes)
      foreach (EcsEntity frog in _chasingFrogs)
      {
        float nextPoint = hero.Get<ViewRef>().View.transform.position.x;
        frog
          .Del<ChaseCommand>()
          .Add<Chasing>()
          .Has<StartJumpCycleCommand>(true)
          .Replace((ref JumpPoint point) => point.Point = nextPoint)
          .Replace((ref FrogJumpContext ctx) =>
          {
            ctx.Length = _config.BigJumpLength;
            ctx.Height = _config.BigJumpHeight;
          });
      }
    }
  }
}