using Cysharp.Threading.Tasks;
using LudensClub.GeoChaos.Runtime.Infrastructure.SceneLoading;
using LudensClub.GeoChaos.Runtime.Persistence;
using LudensClub.GeoChaos.Runtime.Windows.Curtain;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.StateMachineComponents
{
  public class GameplayGameState : IState 
  {
    private readonly ISceneLoader _sceneLoader;
    private readonly IPersistenceService _persistenceSvc;
    private readonly ICurtainPresenter _curtainPresenter;

    public GameplayGameState(ISceneLoader sceneLoader, IPersistenceService persistenceSvc, ICurtainPresenter curtainPresenter)
    {
      _sceneLoader = sceneLoader;
      _persistenceSvc = persistenceSvc;
      _curtainPresenter = curtainPresenter;
    }
      
    public async UniTask Enter()
    {
      await _curtainPresenter.ShowAsync();
        
      await _persistenceSvc.LoadGameAsync();
      await _sceneLoader.LoadAsync(SceneType.Game);
        
      await _curtainPresenter.HideAsync();
    }

    public UniTask Exit()
    {
      return UniTask.CompletedTask;
    }
  }
}