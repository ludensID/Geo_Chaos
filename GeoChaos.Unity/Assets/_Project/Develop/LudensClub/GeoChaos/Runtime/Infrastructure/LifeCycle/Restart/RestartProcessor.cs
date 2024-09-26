using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  public class RestartProcessor : IRestartProcessor
  {
    private List<IRestartable> _restartables = new List<IRestartable>();

    public void Add(IRestartable restartable)
    {
      _restartables.Add(restartable);
    }

    public void Remove(IRestartable restartable)
    {
      _restartables.Remove(restartable);
    }

    public async UniTask RestartAsync()
    {
      foreach (IRestartable restartable in _restartables)
      {
        await restartable.RestartAsync();
      }
    }
  }
}