using System;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Enemy;
using LudensClub.GeoChaos.Runtime.Gameplay.Attack.Feature;
using LudensClub.GeoChaos.Runtime.Gameplay.Creation.Feature;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Features;
using LudensClub.GeoChaos.Runtime.Gameplay.Input;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Feature;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Gameplay
{
  public class Engine : IInitializable, ITickable, IDisposable
  {
    private EcsSystems _systems;

    public Engine(IEcsSystemsFactory systemsFactory, IEcsSystemFactory factory)
    {
      _systems = systemsFactory.Create();

      _systems
        .Add(factory.Create<CreationFeature>())
        .Add(factory.Create<CollisionFeature>())
        .Add(factory.Create<InputFeature>())
        .Add(factory.Create<AttackFeature>())
        .Add(factory.Create<EnemyFeature>())
        .Add(factory.Create<HeroFeature>());
    }

    public void Initialize()
    {
      _systems.Init();
    }

    public void Tick()
    {
      _systems.Run();
    }

    public void Dispose()
    {
      if (_systems != null)
      {
        _systems.Destroy();
        _systems = null;
      }
    }
  }
}