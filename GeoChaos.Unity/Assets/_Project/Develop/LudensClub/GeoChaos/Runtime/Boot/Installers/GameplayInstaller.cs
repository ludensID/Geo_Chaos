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
      Debugging.DebugInstaller.BindEcsWorldDebugEngine(Container);
#endif

      BindEngine();
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