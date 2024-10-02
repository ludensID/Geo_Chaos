using Cysharp.Threading.Tasks;

namespace LudensClub.GeoChaos.Runtime.Windows.Curtain
{
  public interface ICurtainPresenter
  {
    void SetView(CurtainView view);
    UniTask ShowAsync();
    UniTask HideAsync();
  }
}