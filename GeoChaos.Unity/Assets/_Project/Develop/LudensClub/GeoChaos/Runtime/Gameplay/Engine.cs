using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Characteristics;
using LudensClub.GeoChaos.Runtime.Gameplay.AI;
using LudensClub.GeoChaos.Runtime.Gameplay.Attack;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Creation;
using LudensClub.GeoChaos.Runtime.Gameplay.Enemies;
using LudensClub.GeoChaos.Runtime.Gameplay.Environment;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Features;
using LudensClub.GeoChaos.Runtime.Gameplay.Input;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Collisions;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.View;
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
        .Add(factory.Create<EnemyFixedFeature>())
        .Add(factory.Create<ViewFixedFeature>());

      _updateSystems
        .Add(factory.Create<CreationFeature>())
        .Add(factory.Create<CollisionFeature>())
        .Add(factory.Create<InputFeature>())
        .Add(factory.Create<CharacteristicPreparingFeature>())
        .Add(factory.Create<EnvironmentFeature>())
        .Add(factory.Create<HeroFeature>())
        .Add(factory.Create<AIFeature>())
        .Add(factory.Create<EnemyFeature>())
        .Add(factory.Create<AttackFeature>())
        .Add(factory.Create<CharacteristicBoundingFeature>())
        .Add(factory.Create<ViewFeature>())
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