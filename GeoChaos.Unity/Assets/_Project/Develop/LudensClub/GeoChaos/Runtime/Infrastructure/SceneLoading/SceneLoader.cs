using Cysharp.Threading.Tasks;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Windows.Curtain;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.SceneLoading
{
  public class SceneLoader : ISceneLoader
  {
    private readonly CurtainModel _curtainModel;
    private readonly SceneConfig _config;

    public SceneLoader(IConfigProvider configProvider, CurtainModel curtainModel)
    {
      _curtainModel = curtainModel;
      _config = configProvider.Get<SceneConfig>();
    }

    public async UniTask LoadAsync(SceneType id)
    {
      string sceneName = _config.GetSceneName(id);
      AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
      operation.allowSceneActivation = false;
      while (operation.progress < 0.89)
      {
        _curtainModel.Progress.Value = operation.progress;
        await UniTask.Yield();
      }

      operation.allowSceneActivation = true;
    }
  }
}