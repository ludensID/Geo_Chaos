using Cysharp.Threading.Tasks;
using LudensClub.GeoChaos.Runtime.Infrastructure.SceneLoading;
using LudensClub.GeoChaos.Runtime.Persistence;
using LudensClub.GeoChaos.Runtime.Windows.Curtain;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.StateMachineComponents
{
  public class MenuGameState : IState, IPaylodedState<OnlyLoadScenePayload>
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
      await LoadMenu();
    }

    public async UniTask Enter(OnlyLoadScenePayload _)
    {
      await LoadMenu(false);
    }

    public UniTask Exit()
    {
      return UniTask.CompletedTask;
    }

    private async UniTask LoadMenu(bool loadSettings = true)
    {
      await _curtainPresenter.ShowAsync();

      if (loadSettings)
        await _persistenceSvc.LoadSettingsAsync();
      await _sceneLoader.LoadAsync(SceneType.Menu);

      await _curtainPresenter.HideAsync();
    }
  }
}