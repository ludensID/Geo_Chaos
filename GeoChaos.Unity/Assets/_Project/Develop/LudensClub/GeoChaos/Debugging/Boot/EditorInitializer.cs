using Cysharp.Threading.Tasks;
using LudensClub.GeoChaos.Runtime.Infrastructure.SceneLoading;
using LudensClub.GeoChaos.Runtime.Infrastructure.StateMachineComponents;
using UnityEngine.SceneManagement;
using Zenject;

namespace LudensClub.GeoChaos.Debugging.Boot
{
  public class EditorInitializer : IInitializable
  {
    private readonly GameStateMachine _gameStateMachine;

    public EditorInitializer(GameStateMachine gameStateMachine)
    {
      _gameStateMachine = gameStateMachine;
    }

    public void Initialize()
    {
      var payload = new CustomLoadScenePayload { SceneLoader = LoadScene };

      if (PlayModeSceneLoader.CurrentSceneType == SceneType.Game)
        _gameStateMachine.SwitchState<GameplayGameState, CustomLoadScenePayload>(payload).Forget();
      else if (PlayModeSceneLoader.CurrentSceneType == SceneType.Menu)
        _gameStateMachine.SwitchState<MenuGameState, CustomLoadScenePayload>(payload).Forget();
    }

    private async UniTask LoadScene()
    {
      Scene scene = PlayModeSceneLoader.LoadCurrentScene();
      await UniTask.WaitUntil(() => scene.isLoaded);
    }
  }
}