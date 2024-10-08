using System;
using Cysharp.Threading.Tasks;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Windows.Curtain;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.SceneLoading
{
  public class SceneLoader : ISceneLoader, IDisposable
  {
    private readonly CurtainModel _curtainModel;
    private readonly SceneConfig _config;

    private bool _wasSceneChanged;

    public SceneLoader(IConfigProvider configProvider, CurtainModel curtainModel)
    {
      _curtainModel = curtainModel;
      _config = configProvider.Get<SceneConfig>();

      SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }

    private void OnActiveSceneChanged(Scene _, Scene __)
    {
      _wasSceneChanged = true;
    }

    public void Dispose()
    {
      SceneManager.activeSceneChanged -= OnActiveSceneChanged;
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
      _wasSceneChanged = false;
      await UniTask.WaitUntil(() => _wasSceneChanged);
    }
  }
}