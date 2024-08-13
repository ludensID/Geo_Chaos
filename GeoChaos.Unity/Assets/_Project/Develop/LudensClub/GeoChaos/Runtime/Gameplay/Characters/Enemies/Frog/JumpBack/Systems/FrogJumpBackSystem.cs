using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.AI;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Jump;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.JumpCycle;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

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
        frog
          .Del<JumpBackCommand>()
          .Add<JumpingBack>()
          .Add<StartJumpCycleCommand>()
          .Replace((ref JumpPoint jumpPoint) => jumpPoint.Point = frog.Get<StartTransform>().Position.x)
          .Replace((ref FrogJumpContext ctx) =>
          {
            ctx.Length = _config.JumpBackLength;
            ctx.Height = _config.JumpBackHeight;
          });
      }
    }
  }
}