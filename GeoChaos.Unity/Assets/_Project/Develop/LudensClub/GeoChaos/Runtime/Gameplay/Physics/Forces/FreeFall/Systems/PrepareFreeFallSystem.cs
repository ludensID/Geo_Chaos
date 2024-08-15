using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces
{
  public class PrepareFreeFallSystem : IEcsRunSystem
  {
    private readonly IFreeFallService _freeFallSvc;
    private readonly EcsWorld _game;
    private readonly EcsEntities _actionEvents;
    private readonly EcsWorld _physics;
    private readonly EcsEntities _freeFalls;

    public PrepareFreeFallSystem(GameWorldWrapper gameWorldWrapper,
      PhysicsWorldWrapper physicsWorldWrapper,
      IFreeFallService freeFallSvc)
    {
      _freeFallSvc = freeFallSvc;
      _game = gameWorldWrapper.World;
      _physics = physicsWorldWrapper.World;

      _actionEvents = _game
        .Filter<OnActionStarted>()
        .Collect();

      _freeFalls = _physics
        .Filter<FreeFall>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity action in _actionEvents)
      {
        ref OnActionStarted startedAction = ref action.Get<OnActionStarted>();
        if (action.Has<FreeFalling>())
          _freeFallSvc.StopFreeFall(action, _freeFalls);
        
        if(!startedAction.IsEmpty)
        {
          foreach (EcsEntity freeFall in _freeFalls
            .Check<Owner>(owner => owner.Entity.EqualsTo(action.PackedEntity)))
          {
            freeFall.Add<PrepareCommand>();
          }
        }
      }
    }
  }
}