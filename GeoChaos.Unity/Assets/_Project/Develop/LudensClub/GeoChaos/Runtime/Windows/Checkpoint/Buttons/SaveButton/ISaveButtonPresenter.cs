using Cysharp.Threading.Tasks;

namespace LudensClub.GeoChaos.Runtime.Windows.Checkpoint
{
  public interface ISaveButtonPresenter
  {
    UniTask SaveAsync();
  }
}