using System;
using Cysharp.Threading.Tasks;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Windows.Curtain
{
  public class CurtainPresenter : ICurtainPresenter, IInitializable, IDisposable
  {
    private readonly CurtainModel _model;
    private readonly CurtainConfig _config;
    private CurtainView _view;

    public CurtainPresenter(IExplicitInitializer initializer, IConfigProvider configProvider, CurtainModel model)
    {
      _model = model;
      _config = configProvider.Get<CurtainConfig>();
      
      initializer.Add(this);
      _model.Progress.OnChanged += OnProgressChanged;
    }

    public void SetView(CurtainView view)
    {
      _view = view;
    }

    public void Initialize()
    {
      _view.gameObject.SetActive(false);
    }

    public async UniTask ShowAsync()
    {
      await UniTask.Delay(TimeSpan.FromSeconds(_config.TimeToOpen), true);
      _view.gameObject.SetActive(true);
      OnProgressChanged();
    }

    public async UniTask HideAsync()
    {
      await UniTask.Delay(TimeSpan.FromSeconds(_config.TimeToClose), true);
      _view.gameObject.SetActive(false);
    }

    private void OnProgressChanged()
    {
      if (_view.gameObject.activeSelf)
      {
        _view.SetSliderValue(_model.Progress.Value);
      }
    }

    public void Dispose()
    {
      _model.Progress.OnChanged -= OnProgressChanged;
    }
  }
}