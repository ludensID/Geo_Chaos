using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces
{
  public class PrepareADControlSystem : IEcsRunSystem
  {
    private readonly IFreeFallService _freeFallSvc;
    private readonly EcsWorld _game;
    private readonly EcsWorld _physics;
    private readonly HeroConfig _config;
    private readonly EcsEntities _actionEvents;
    private readonly EcsEntities _controls;

    public PrepareADControlSystem(GameWorldWrapper gameWorldWrapper,
      PhysicsWorldWrapper physicsWorldWrapper,
      IConfigProvider configProvider,
      IFreeFallService freeFallSvc)
    {
      _freeFallSvc = freeFallSvc;
      _game = gameWorldWrapper.World;
      _physics = physicsWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _actionEvents = _game
        .Filter<OnActionStarted>()
        .Inc<ADControllable>()
        .Collect();

      _controls = _physics
        .Filter<ADControl>()
        .Inc<PrepareCommand>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity action in _actionEvents)
      {
        ref OnActionStarted startedAction = ref action.Get<OnActionStarted>();

        foreach (EcsEntity control in _controls
          .Where<Owner>(owner => owner.Entity.EqualsTo(action.Pack())))
        {
          _freeFallSvc.PrepareFreeFall(control, startedAction.Time, _config.StartADControlCoefficient,
            _config.UseADControlGradient);

          control.Replace((ref ControlSpeed speed) => speed.Speed = 0);
        }
      }
    }
  }
}