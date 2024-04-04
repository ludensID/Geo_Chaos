using LudensClub.GeoChaos.Runtime.Gameplay;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
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
      BindInputWorldWrapper();
      BindGameWorldWrapper();
      BindPlayerFactory();

#if UNITY_EDITOR
      Debugging.DebugInstaller.BindEcsWorldDebugEngine(Container);
      Debugging.DebugInstaller.BindInputWatcherDebug(Container);
      Debugging.DebugInstaller.BindGravityScaleWatcher(Container);
#endif

      BindEngine();
    }

    private void BindInputWorldWrapper()
    {
      Container
        .BindInterfacesAndSelfTo<InputWorldWrapper>()
        .AsSingle();
    }

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