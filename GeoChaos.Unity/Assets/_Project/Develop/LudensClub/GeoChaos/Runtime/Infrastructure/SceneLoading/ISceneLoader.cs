using Cysharp.Threading.Tasks;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.SceneLoading
{
  public interface ISceneLoader
  {
    UniTask LoadAsync(SceneType id);
  }
}