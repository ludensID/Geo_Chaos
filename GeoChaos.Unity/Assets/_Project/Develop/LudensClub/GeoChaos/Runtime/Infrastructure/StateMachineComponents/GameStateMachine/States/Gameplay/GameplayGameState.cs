using System;
using Cysharp.Threading.Tasks;
using LudensClub.GeoChaos.Runtime.Infrastructure.SceneLoading;
using LudensClub.GeoChaos.Runtime.Persistence;
using LudensClub.GeoChaos.Runtime.Windows.Curtain;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.StateMachineComponents
{
  public class GameplayGameState : IState, IPaylodedState<StartNewGamePayload>, IPaylodedState<CustomLoadScenePayload>
  {
    private readonly ISceneLoader _sceneLoader;
    private readonly IPersistenceService _persistenceSvc;
    private readonly ICurtainPresenter _curtainPresenter;

    public GameplayGameState(ISceneLoader sceneLoader,
      IPersistenceService persistenceSvc,
      ICurtainPresenter curtainPresenter)
    {
      _sceneLoader = sceneLoader;
      _persistenceSvc = persistenceSvc;
      _curtainPresenter = curtainPresenter;
    }

    public async UniTask Enter()
    {
      await StartGameplay();
    }

    public async UniTask Enter(StartNewGamePayload _)
    {
      await StartGameplay(true);
    }

    public async UniTask Enter(CustomLoadScenePayload payload)
    {
      await StartGameplay(sceneLoader: payload.SceneLoader);
    }

    public UniTask Exit()
    {
      return UniTask.CompletedTask;
    }

    private async UniTask StartGameplay(bool startNewGame = false, Func<UniTask> sceneLoader = null)
    {
      await _curtainPresenter.ShowAsync();

      if (startNewGame)
        _persistenceSvc.ResetGamePersistence();
      else
        await _persistenceSvc.LoadGameAsync();

      if (sceneLoader != null)
        await sceneLoader.Invoke();
      else
        await _sceneLoader.LoadAsync(SceneType.Game);

      await _curtainPresenter.HideAsync();
    }
  }
}