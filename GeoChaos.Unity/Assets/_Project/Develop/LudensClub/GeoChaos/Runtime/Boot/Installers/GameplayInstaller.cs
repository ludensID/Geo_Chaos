using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Props;
using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Boot
{
  public class GameplayInstaller : MonoInstaller
  {
    public override void InstallBindings()
    {
      BindEcsSystemFactory(); 
      BindEcsWorldProvider();
      BindPlayerFactory();
      BindEngine();
    }
    
    private void BindEcsSystemFactory()
    {
      Container
        .Bind<IEcsSystemFactory>()
        .To<EcsSystemFactory>()
        .AsSingle();
    }

    private void BindEcsWorldProvider()
    {
      Container
        .Bind<IEcsWorldProvider>()
        .To<EcsWorldProvider>()
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
        .Bind<IPlayerFactory>()
        .To<PlayerFactory>()
        .AsSingle();
    }
  }
}