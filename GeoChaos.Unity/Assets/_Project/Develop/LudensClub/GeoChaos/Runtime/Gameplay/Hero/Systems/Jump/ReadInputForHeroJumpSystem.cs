using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Input;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class ReadInputForHeroJumpSystem : IEcsRunSystem
  {
    private readonly IInputDataProvider _input;
    private readonly EcsWorld _world;
    private readonly EcsFilter _grounds;
    private readonly EcsFilter _noStoppeds;
    private readonly EcsFilter _jumpStartedInputs;
    private readonly EcsFilter _jumpCanceledInputs;

    public ReadInputForHeroJumpSystem(GameWorldWrapper gameWorldWrapper, InputWorldWrapper inputWorldWrapper, IInputDataProvider input)
    {
      _input = input;
      _world = gameWorldWrapper.World;
      EcsWorld inputWorld = inputWorldWrapper.World;

      _grounds = _world.Filter<Hero>()
        .Inc<JumpAvailable>()
        .Inc<Ground>()
        .End();

      _jumpStartedInputs = inputWorld
        .Filter<Expired>()
        .Inc<IsJumpStarted>()
        .End();

      _noStoppeds = _world.Filter<Hero>()
        .Inc<JumpAvailable>()
        .Inc<IsJumping>()
        .Exc<WaitToStopJump>()
        .End();

      _jumpCanceledInputs = inputWorld
        .Filter<Expired>()
        .Inc<IsJumpCanceled>()
        .End();
    }

    public void Run(EcsSystems systems)
    {
      // if(_input.Data.IsJumpStarted)
      foreach (int _ in _jumpStartedInputs)
        foreach (int hero in _grounds)
        {
          ref Ground ground = ref _world.Get<Ground>(hero);
          if (ground.IsOnGround)
            _world.Add<JumpCommand>(hero);
        }
      
      // if(_input.Data.IsJumpCanceled)
      foreach (int _ in _jumpCanceledInputs)
        foreach (int noStopped in _noStoppeds)
        {
          _world.Add<StopJumpCommand>(noStopped);
        }
    }
  }
}