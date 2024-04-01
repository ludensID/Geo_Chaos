using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class ReadInputForHeroJumpSystem : IEcsRunSystem
  {
    private readonly IInputDataProvider _inputProvider;
    private readonly EcsWorld _world;
    private readonly EcsFilter _grounds;
    private readonly EcsFilter _noStoppeds;

    public ReadInputForHeroJumpSystem(GameWorldWrapper gameWorldWrapper, IInputDataProvider inputProvider)
    {
      _inputProvider = inputProvider;
      _world = gameWorldWrapper.World;

      _grounds = _world.Filter<Hero>()
        .Inc<JumpAvailable>()
        .Inc<Ground>()
        .End();

      _noStoppeds = _world.Filter<Hero>()
        .Inc<JumpAvailable>()
        .Exc<WaitToStopJump>()
        .End();
    }

    public void Run(EcsSystems systems)
    {
      foreach (int hero in _grounds)
      {
        ref Ground ground = ref _world.Get<Ground>(hero);
        if (_inputProvider.Data.IsJumpStarted && ground.IsOnGround)
          _world.Add<JumpCommand>(hero);
      }

      foreach (int noStopped in _noStoppeds)
      {
        ref JumpAvailable jump = ref _world.Get<JumpAvailable>(noStopped);
        if (_inputProvider.Data.IsJumpCanceled && jump.IsJumping)
          _world.Add<StopJumpCommand>(noStopped);
      }
    }
  }
}