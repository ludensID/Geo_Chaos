using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces
{
  public class PrepareFallFreeSystem : IEcsRunSystem
  {
    private readonly IDragForceService _dragForceSvc;
    private readonly ITimerFactory _timers;
    private readonly IADControlService _controlSvc;
    private readonly EcsWorld _game;
    private readonly EcsEntities _prepares;
    private readonly HeroConfig _config;

    public PrepareFallFreeSystem(GameWorldWrapper gameWorldWrapper,
      IDragForceService dragForceSvc,
      IConfigProvider configProvider,
      ITimerFactory timers,
      IADControlService controlSvc)
    {
      _dragForceSvc = dragForceSvc;
      _timers = timers;
      _controlSvc = controlSvc;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _prepares = _game
        .Filter<PrepareFallFreeCommand>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity prepare in _prepares)
      {
        ref PrepareFallFreeCommand command = ref prepare.Get<PrepareFallFreeCommand>();
        float time = command.Time;
        Vector2 velocity = command.Velocity;
        float controlTime = time * 2 * (1 - _config.StartDragForceCoefficient * 2);
        controlTime = MathUtils.Clamp(controlTime, 0.0001f);

        if (prepare.Has<DragForceAvailable>())
        {
          EcsEntity drag = _dragForceSvc.GetDragForce(prepare.Pack());
          if (_config.UseDragForceGradient)
          {
            drag.Add((ref Delay delay) =>
              delay.TimeLeft = _timers.Create(time * 2 * _config.StartDragForceCoefficient));
          }

          drag
            .Add<Prepared>()
            .Replace((ref GradientRate rate) => rate.Rate = 1 / controlTime)
            .Replace((ref RelativeSpeed relative) =>
              relative.Speed = new Vector2(Mathf.Abs(velocity.x), Mathf.Abs(velocity.y)));
        }

        if (prepare.Has<ADControllable>())
        {
          EcsEntity control = _controlSvc.GetADControl(prepare.Pack());
          if (_config.UseADControlGradient)
          {
            control.Add((ref Delay delay) =>
              delay.TimeLeft = _timers.Create(time * 2 * _config.StartADControlCoefficient));
          }

          control
            .Add<Prepared>()
            .Replace((ref GradientRate rate) => rate.Rate = 1 / controlTime)
            .Replace((ref ControlSpeed speed) => speed.Speed = 0);
        }

        prepare.Del<PrepareFallFreeCommand>();
      }
    }
  }
}