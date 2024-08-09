using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Jump;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Gravity;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Gravity.Tracking;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Jump
{
  public class FrogJumpSystem : IEcsRunSystem
  {
    private readonly ISpeedForceFactory _forceFactory;
    private readonly EcsWorld _game;
    private readonly EcsEntities _jumpingFrogs;

    public FrogJumpSystem(GameWorldWrapper gameWorldWrapper, ISpeedForceFactory forceFactory)
    {
      _forceFactory = forceFactory;
      _game = gameWorldWrapper.World;

      _jumpingFrogs = _game
        .Filter<FrogTag>()
        .Inc<JumpCommand>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity frog in _jumpingFrogs)
      {
        ref FrogJumpContext ctx = ref frog.Get<FrogJumpContext>();
        float gravityScale = frog.Get<GravityScale>().Scale;

        float time = 2 * Mathf.Sqrt(2 * ctx.Height / Mathf.Abs(gravityScale * Physics2D.gravity.y));
        _forceFactory.Create(new SpeedForceData(SpeedForceType.Jump, frog.PackedEntity, Vector2.one)
        {
          Speed = new Vector2(ctx.Length / time, ctx.Height / (time / 2)),
          Direction = new Vector2(ctx.Direction, 1),
          Instant = true
        });

        frog
          .Del<JumpCommand>()
          .Has<Jumping>(true)
          .Add<TrackLandingCommand>();
      }
    }
  }
}