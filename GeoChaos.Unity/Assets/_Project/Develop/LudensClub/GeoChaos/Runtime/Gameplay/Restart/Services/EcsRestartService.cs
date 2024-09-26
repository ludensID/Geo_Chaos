using Cysharp.Threading.Tasks;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Restart
{
  public class EcsRestartService : IRestartable, IEcsRestartService
  {
    private readonly EcsWorld _message;
    private bool _restarting; 

    public EcsRestartService(IRestartProcessor restartProcessor, MessageWorldWrapper messageWorldWrapper)
    {
      restartProcessor.Add(this);
      _message = messageWorldWrapper.World;
    }

    public async UniTask RestartAsync()
    {
      _message
        .CreateEntity()
        .Add<RestartLevelMessage>();
      
      _restarting = true;
      await UniTask.WaitWhile(() => _restarting);
    }

    public void FinishRestart()
    {
      _restarting = false;
    }
  }
}