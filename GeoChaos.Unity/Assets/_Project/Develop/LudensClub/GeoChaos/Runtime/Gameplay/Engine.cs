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
        .Add(factory.Create<HeroPhysicsFeature>());

      _updateSystems
        .Add(factory.Create<CreationFeature>())
        .Add(factory.Create<CollisionFeature>())
        .Add(factory.Create<InputFeature>())
        .Add(factory.Create<AttackFeature>())
        .Add(factory.Create<EnemyFeature>())
        .Add(factory.Create<HeroFeature>());

      _lateUpdateSystems
        .Add(factory.Create<LateHeroFeature>());
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