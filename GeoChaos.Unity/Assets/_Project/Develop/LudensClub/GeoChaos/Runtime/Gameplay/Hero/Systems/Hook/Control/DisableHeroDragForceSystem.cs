using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Move;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Hook
{
  public class DisableHeroDragForceSystem : IEcsRunSystem
  {
    private readonly IDragForceService _dragForceSvc;
    private readonly IADControlService _controlSvc;
    private readonly ISpeedForceFactory _forceFactory;
    private readonly EcsWorld _game;
    private readonly EcsEntities _onGrounds;

    public DisableHeroDragForceSystem(GameWorldWrapper gameWorldWrapper,
      IDragForceService dragForceSvc,
      IADControlService controlSvc,
      ISpeedForceFactory forceFactory)
    {
      _dragForceSvc = dragForceSvc;
      _controlSvc = controlSvc;
      _forceFactory = forceFactory;
      _game = gameWorldWrapper.World;

      _onGrounds = _game
        .Filter<HookFalling>()
        .Inc<OnGround>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity ground in _onGrounds)
      {
        _forceFactory.Create(new SpeedForceData(SpeedForceType.Hook, ground.Pack()));
        if (ground.Has<DragForceAvailable>())
        {
          _dragForceSvc.GetDragForce(ground.Pack())
            .Has<Enabled>(false);
        }

        if (ground.Has<ADControllable>())
        {
          ground.Del<FreeRotating>();
          _controlSvc.GetADControl(ground.Pack())
            .Has<Enabled>(false);
        }
      }
    }
  }
}