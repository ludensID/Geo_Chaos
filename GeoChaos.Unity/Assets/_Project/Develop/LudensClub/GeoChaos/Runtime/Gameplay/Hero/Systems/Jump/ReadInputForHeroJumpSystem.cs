using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Lock;
using LudensClub.GeoChaos.Runtime.Gameplay.Input;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class ReadInputForHeroJumpSystem : IEcsRunSystem
  {
    private readonly EcsWorld _world;
    private readonly EcsEntities _grounds;
    private readonly EcsEntities _jumpStartedInputs;
    private readonly EcsEntities _noStoppeds;
    private readonly EcsEntities _jumpCanceledInputs;

    public ReadInputForHeroJumpSystem(GameWorldWrapper gameWorldWrapper, InputWorldWrapper inputWorldWrapper)
    {
      _world = gameWorldWrapper.World;
      EcsWorld inputWorld = inputWorldWrapper.World;

      _grounds = _world.Filter<HeroTag>()
        .Inc<JumpAvailable>()
        .Inc<OnGround>()
        .Exc<MovementLocked>()
        .Collect();

      _jumpStartedInputs = inputWorld
        .Filter<Expired>()
        .Inc<IsJumpStarted>()
        .Collect();

      _noStoppeds = _world.Filter<HeroTag>()
        .Inc<JumpAvailable>()
        .Inc<Jumping>()
        .Exc<MovementLocked>()
        .Collect();

      _jumpCanceledInputs = inputWorld
        .Filter<Expired>()
        .Inc<IsJumpCanceled>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity _ in _jumpStartedInputs)
      foreach (EcsEntity hero in _grounds)
        hero.Add<JumpCommand>();

      foreach (EcsEntity _ in _jumpCanceledInputs)
      foreach (EcsEntity noStopped in _noStoppeds)
        noStopped.Add<StopJumpCommand>();
    }
  }
}