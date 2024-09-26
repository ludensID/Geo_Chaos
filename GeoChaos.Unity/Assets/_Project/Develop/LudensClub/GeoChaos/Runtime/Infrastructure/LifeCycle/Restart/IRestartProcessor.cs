using Cysharp.Threading.Tasks;

namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  public interface IRestartProcessor
  {
    void Add(IRestartable restartable);
    void Remove(IRestartable restartable);
    UniTask RestartAsync();
  }
}