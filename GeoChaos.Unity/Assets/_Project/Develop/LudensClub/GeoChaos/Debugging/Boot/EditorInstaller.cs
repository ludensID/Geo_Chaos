using Zenject;

namespace LudensClub.GeoChaos.Debugging.Boot
{
  public class EditorInstaller : MonoInstaller, IInitializable
  {
    public override void InstallBindings()
    {
      Container
        .Bind<IInitializable>()
        .FromInstance(this)
        .AsSingle();
    }

    public void Initialize()
    {
      PlayModeSceneLoader.LoadCurrentScene();
    }
  }
}