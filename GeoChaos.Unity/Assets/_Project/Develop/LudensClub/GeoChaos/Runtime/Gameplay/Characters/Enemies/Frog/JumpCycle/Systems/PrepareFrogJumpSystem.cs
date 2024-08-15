using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Jump;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.JumpWait;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Jump;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.JumpCycle
{
  public class PrepareFrogJumpSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _cyclingFrogs;
    private readonly EcsEntities _afterJumpCyclingFrogs;

    public PrepareFrogJumpSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _afterJumpCyclingFrogs = _game
        .Filter<FrogTag>()
        .Inc<JumpCycling>()
        .Inc<OnJumpWaitFinished>()
        .Collect();

      _cyclingFrogs = _game
        .Filter<FrogTag>()
        .Inc<JumpCycling>()
        .Inc<PrepareJumpCommand>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity frog in _afterJumpCyclingFrogs)
      {
        frog.Has<PrepareJumpCommand>(true);
      }

      foreach (EcsEntity frog in _cyclingFrogs)
      {
        frog.Del<PrepareJumpCommand>();
        PrepareJump(frog);
      }
    }

    private void PrepareJump(EcsEntity frog)
    {
      float currentPoint = frog.Get<ViewRef>().View.transform.position.x;
      float nextPoint = frog.Get<JumpPoint>().Point;

      frog
        .Add<JumpCommand>()
        .Change((ref FrogJumpContext ctx) => ctx.Direction = Mathf.Sign(nextPoint - currentPoint));
    }
  }
}