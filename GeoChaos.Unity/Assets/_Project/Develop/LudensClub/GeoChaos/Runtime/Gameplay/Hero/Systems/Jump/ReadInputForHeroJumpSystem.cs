using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Lock;
using LudensClub.GeoChaos.Runtime.Gameplay.Input;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class ReadInputForHeroJumpSystem : IEcsRunSystem
  {
    private readonly EcsWorld _world;
    private readonly EcsFilter _grounds;
    private readonly EcsFilter _noStoppeds;
    private readonly EcsFilter _jumpStartedInputs;
    private readonly EcsFilter _jumpCanceledInputs;

    public ReadInputForHeroJumpSystem(GameWorldWrapper gameWorldWrapper, InputWorldWrapper inputWorldWrapper)
    {
      _world = gameWorldWrapper.World;
      EcsWorld inputWorld = inputWorldWrapper.World;

      _grounds = _world.Filter<HeroTag>()
        .Inc<JumpAvailable>()
        .Inc<OnGround>()
        .Exc<MovementLocked>()
        .End();

      _jumpStartedInputs = inputWorld
        .Filter<Expired>()
        .Inc<IsJumpStarted>()
        .End();

      _noStoppeds = _world.Filter<HeroTag>()
        .Inc<JumpAvailable>()
        .Inc<Jumping>()
        .Exc<MovementLocked>()
        .End();

      _jumpCanceledInputs = inputWorld
        .Filter<Expired>()
        .Inc<IsJumpCanceled>()
        .End();
    }

    public void Run(EcsSystems systems)
    {
      foreach (var _ in _jumpStartedInputs)
      foreach (var hero in _grounds)
        _world.Add<JumpCommand>(hero);

      foreach (var _ in _jumpCanceledInputs)
      foreach (var noStopped in _noStoppeds)
        _world.Add<StopJumpCommand>(noStopped);
    }
  }
}