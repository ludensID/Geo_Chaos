using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero
{
  public class ConvertDelayedToJustComponentSystem<TDelayedComponent, TComponent> : IEcsRunSystem
    where TDelayedComponent : struct, IEcsComponent where TComponent : struct, IEcsComponent
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _delayedComponents;

    public ConvertDelayedToJustComponentSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _delayedComponents = _game
        .Filter<HeroTag>()
        .Inc<TDelayedComponent>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity component in _delayedComponents)
      {
        component
          .Del<TDelayedComponent>()
          .Add<TComponent>();
      }
    }
  }
}