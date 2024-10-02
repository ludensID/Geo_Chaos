using LudensClub.GeoChaos.Runtime.Windows.Curtain;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Boot
{
  public class CurtainInstaller : Installer<CurtainInstaller>
  {
    public override void InstallBindings()
    {
      BindCurtainModel();
      BindCurtainPresenter();
    }

    private void BindCurtainModel()
    {
      Container
        .Bind<CurtainModel>()
        .AsSingle();
    }

    private void BindCurtainPresenter()
    {
      Container
        .Bind<ICurtainPresenter>()
        .To<CurtainPresenter>()
        .AsSingle();
    }
  }
}