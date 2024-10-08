using LudensClub.GeoChaos.Editor.Persistence;
using LudensClub.GeoChaos.Runtime.Persistence;
using Zenject;

namespace LudensClub.GeoChaos.Editor.Boot
{
  public class DebugProjectInstaller : Installer<DebugProjectInstaller>
  {
    public override void InstallBindings()
    {
      BindInputDebug();
      RebindGameDataLoader();
    }

    private void BindInputDebug()
    {
      Container
        .Bind<InputDebug>()
        .FromNewComponentOnNewGameObject()
        .WithGameObjectName($"[{nameof(InputDebug)}]")
        .AsSingle()
        .NonLazy();
    }

    private void RebindGameDataLoader()
    {
      Container
        .Rebind<IGamePersistenceLoader>()
        .To<DebugGamePersistenceLoader>()
        .AsSingle();
    }
  }
}