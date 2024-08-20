using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Patrol;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Jump;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.JumpCycle;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.JumpBack
{
  public class FrogJumpBackSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _jumpingFrogs;
    private readonly FrogConfig _config;

    public FrogJumpBackSystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider)
    {
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<FrogConfig>();

      _jumpingFrogs = _game
        .Filter<FrogTag>()
        .Inc<JumpBackCommand>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity frog in _jumpingFrogs)
      {
        Vector2 bounds = frog.Get<PatrolBounds>().HorizontalBounds;
        float center = (bounds.x + bounds.y) / 2;
          
        frog
          .Del<JumpBackCommand>()
          .Add<JumpingBack>()
          .Add<StartJumpCycleCommand>()
          .Replace((ref JumpPoint jumpPoint) => jumpPoint.Point = center)
          .Replace((ref FrogJumpContext ctx) =>
          {
            ctx.Length = _config.JumpBackLength;
            ctx.Height = _config.JumpBackHeight;
          });
      }
    }
  }
}