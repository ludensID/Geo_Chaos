using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Attack.Feature;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Creation.Feature;
using LudensClub.GeoChaos.Runtime.Gameplay.Enemy;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Features;
using LudensClub.GeoChaos.Runtime.Gameplay.Input;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Collisions;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Gameplay
{
  public class Engine : IInitializable, IFixedTickable, ITickable, ILateTickable
  {
    private readonly EcsSystems _fixedUpdateSystems;
    private readonly EcsSystems _updateSystems;
    private readonly EcsSystems _lateUpdateSystems;

    public Engine(IEcsSystemsFactory systemsFactory, IEcsSystemFactory factory)
    {
      _fixedUpdateSystems = systemsFactory.Create();
      _updateSystems = systemsFactory.Create();
      _lateUpdateSystems = systemsFactory.Create(); 

      _fixedUpdateSystems
        .Add(factory.Create<ViewReadFixedFeature>())
        .Add(factory.Create<FreeFallFeature>())
        .Add(factory.Create<SpeedForceFeature>())
        .Add(factory.Create<ApplyFreeFallFeature>())
        .Add(factory.Create<HeroFixedFeature>())
        .Add(factory.Create<ViewFixedFeature>());

      _updateSystems
        .Add(factory.Create<CreationFeature>())
        .Add(factory.Create<CollisionFeature>())
        .Add(factory.Create<InputFeature>())
        .Add(factory.Create<AttackFeature>())
        .Add(factory.Create<EnemyFeature>())
        .Add(factory.Create<HeroFeature>())
        .Add(factory.Create<CleanupFeature>());

      _lateUpdateSystems
        .Add(factory.Create<HeroLateFeature>());
    }

    public void Initialize()
    {
      _fixedUpdateSystems.Init();
      _updateSystems.Init();
      _lateUpdateSystems.Init();
    }

    public void FixedTick()
    {
      _fixedUpdateSystems.Run();
    }

    public void Tick()
    {
      _updateSystems.Run();
    }

    public void LateTick()
    {
      _lateUpdateSystems.Run();
    }
  }
}