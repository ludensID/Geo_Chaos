using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Hook
{
  public class DisableHeroDragForceSystem : IEcsRunSystem
  {
    private readonly IDragForceService _dragForceSvc;
    private readonly IADControlService _controlSvc;
    private readonly EcsWorld _game;
    private readonly EcsEntities _onGrounds;

    public DisableHeroDragForceSystem(GameWorldWrapper gameWorldWrapper,
      IDragForceService dragForceSvc,
      IADControlService controlSvc)
    {
      _dragForceSvc = dragForceSvc;
      _controlSvc = controlSvc;
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
        // _forceFactory.Create(new SpeedForceData(SpeedForceType.Hook, draggable.Pack(), Vector2.right));
        if (ground.Has<DragForceAvailable>())
        {
          _dragForceSvc.GetDragForce(ground.Pack())
            .Has<Enabled>(false);
        }

        if (ground.Has<ADControllable>())
        {
          _controlSvc.GetADControl(ground.Pack())
            .Has<Enabled>(false);
        }
      }
    }
  }
}