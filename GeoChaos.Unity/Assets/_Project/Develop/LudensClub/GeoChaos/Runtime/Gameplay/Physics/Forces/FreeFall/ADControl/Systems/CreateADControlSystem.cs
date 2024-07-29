using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces
{
  public class CreateADControlSystem : IEcsRunSystem
  {
    private readonly IADControlService _controlSvc;
    private readonly EcsWorld _game;
    private readonly EcsWorld _physics;
    private readonly EcsEntities _controllables;

    public CreateADControlSystem(GameWorldWrapper gameWorldWrapper, PhysicsWorldWrapper physicsWorldWrapper, IADControlService controlSvc)
    {
      _controlSvc = controlSvc;
      _game = gameWorldWrapper.World;
      _physics = physicsWorldWrapper.World;

      _controllables = _game
        .Filter<ADControllable>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity controllable in _controllables)
      {
        if (_controlSvc.GetADControl(controllable.Pack()) == null)
        {
          _physics.CreateEntity()
            .Add<FreeFall>()
            .Add<ADControl>()
            .Add((ref Owner owner) => owner.Entity = controllable.Pack())
            .Add<Gradient>()
            .Add<GradientRate>()
            .Add<ControlSpeed>();
        }
      }
    }
  }
}