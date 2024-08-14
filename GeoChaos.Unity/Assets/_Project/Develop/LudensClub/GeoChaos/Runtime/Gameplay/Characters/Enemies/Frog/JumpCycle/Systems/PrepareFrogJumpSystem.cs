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
    private readonly EcsEntities _startCyclingFrogs;
    private readonly EcsEntities _cyclingFrogs;

    public PrepareFrogJumpSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _startCyclingFrogs = _game
        .Filter<FrogTag>()
        .Inc<StartJumpCycleCommand>()
        .Collect();

      _cyclingFrogs = _game
        .Filter<FrogTag>()
        .Inc<JumpCycling>()
        .Inc<OnJumpWaitFinished>()
        .Exc<JumpCommand>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity frog in _startCyclingFrogs)
      {
        frog
          .Del<StartJumpCycleCommand>()
          .Add<JumpCycling>();
        
        PrepareJump(frog);
      }

      foreach (EcsEntity frog in _cyclingFrogs)
      {
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