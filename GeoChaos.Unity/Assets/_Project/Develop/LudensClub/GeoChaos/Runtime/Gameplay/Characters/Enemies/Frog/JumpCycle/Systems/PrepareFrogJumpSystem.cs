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
    private readonly EcsEntities _startPatrollingFrogs;
    private readonly EcsEntities _patrollingFrogs;

    public PrepareFrogJumpSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _startPatrollingFrogs = _game
        .Filter<FrogTag>()
        .Inc<StartJumpCycleCommand>()
        .Collect();

      _patrollingFrogs = _game
        .Filter<FrogTag>()
        .Inc<JumpCycling>()
        .Inc<OnJumpWaitFinished>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity frog in _startPatrollingFrogs)
      {
        frog
          .Del<StartJumpCycleCommand>()
          .Add<JumpCycling>();
        
        PrepareJump(frog);
      }

      foreach (EcsEntity frog in _patrollingFrogs)
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