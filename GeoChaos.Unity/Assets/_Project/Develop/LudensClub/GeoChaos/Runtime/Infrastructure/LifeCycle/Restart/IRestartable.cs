using Cysharp.Threading.Tasks;

namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  public interface IRestartable
  {
    UniTask RestartAsync();
  }
}