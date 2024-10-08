using Cysharp.Threading.Tasks;
using LudensClub.GeoChaos.Runtime.Boot;
using LudensClub.GeoChaos.Runtime.Infrastructure.SceneLoading;
using LudensClub.GeoChaos.Runtime.Infrastructure.StateMachineComponents;
using UnityEngine.SceneManagement;
using Zenject;

namespace LudensClub.GeoChaos.Editor.Boot
{
  public class DebugBootInitializer : IInitializable
  {
    private readonly GameStateMachine _gameStateMachine;
    private readonly BootInitializer _bootInitializer;

    public DebugBootInitializer(GameStateMachine gameStateMachine)
    {
      _gameStateMachine = gameStateMachine;
      _bootInitializer = new BootInitializer(gameStateMachine);
    }

    public void Initialize()
    {
      if (PlayModeSceneLoader.CurrentSceneType == SceneType.Boot)
      {
        _bootInitializer.Initialize();
        return;
      }
        
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