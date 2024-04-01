using LudensClub.GeoChaos.Runtime.Debugging;
using LudensClub.GeoChaos.Runtime.Gameplay;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Props;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Boot
{
  public class GameplayInstaller : MonoInstaller
  {
    public override void InstallBindings()
    {
      BindEcsSystemFactory();
      BindGameWorldWrapper();
      BindPlayerFactory();

#if UNITY_EDITOR
      BindEcsWorldDebugEngine();
#endif

      BindEngine();
    }

#if UNITY_EDITOR
    private void BindEcsWorldDebugEngine()
    {
      Container
        .BindInterfacesTo<EcsWorldDebugEngine>()
        .AsSingle()
        .NonLazy();
    }
#endif

    private void BindGameWorldWrapper()
    {
      Container
        .BindInterfacesAndSelfTo<GameWorldWrapper>()
        .AsSingle();
    }

    private void BindEcsSystemFactory()
    {
      Container
        .Bind<IEcsSystemFactory>()
        .To<EcsSystemFactory>()
        .AsSingle();
    }

    private void BindEngine()
    {
      Container
        .BindInterfacesTo<Engine>()
        .AsSingle();
    }

    private void BindPlayerFactory()
    {
      Container
        .Bind<IHeroFactory>()
        .To<HeroFactory>()
        .AsSingle();
    }
  }
}