using Cysharp.Threading.Tasks;
using LudensClub.GeoChaos.Runtime.Infrastructure.SceneLoading;
using LudensClub.GeoChaos.Runtime.Persistence;
using LudensClub.GeoChaos.Runtime.Windows.Curtain;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.StateMachineComponents
{
  public class MenuGameState : IState
  {
    private readonly IPersistenceService _persistenceSvc;
    private readonly ISceneLoader _sceneLoader;
    private readonly ICurtainPresenter _curtainPresenter;

    public MenuGameState(IPersistenceService persistenceSvc,
      ISceneLoader sceneLoader,
      ICurtainPresenter curtainPresenter)
    {
      _persistenceSvc = persistenceSvc;
      _sceneLoader = sceneLoader;
      _curtainPresenter = curtainPresenter;
    }

    public async UniTask Enter()
    {
      await _curtainPresenter.ShowAsync();

      await _persistenceSvc.LoadSettingsAsync();
      await _sceneLoader.LoadAsync(SceneType.Menu);

      await _curtainPresenter.HideAsync();
    }

    public UniTask Exit()
    {
      return UniTask.CompletedTask;
    }
  }
}