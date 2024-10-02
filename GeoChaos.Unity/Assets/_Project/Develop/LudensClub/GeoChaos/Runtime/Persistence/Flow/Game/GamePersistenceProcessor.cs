using Cysharp.Threading.Tasks;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Persistence
{
  public class GamePersistenceProcessor : IGamePersistenceProcessor, ITickable
  {
    private readonly IGamePersistenceLoader _loader;
    private readonly ITimerFactory _timers;
    private readonly PersistenceConfig _config;
    private bool _isDirty;
    private Timer _saveDelay = 0;
    private bool _isSaving;

    public GamePersistenceProcessor(IGamePersistenceLoader loader, ITimerFactory timers, IConfigProvider configProvider)
    {
      _loader = loader;
      _timers = timers;

      _config = configProvider.Get<PersistenceConfig>();
    }

    public void SetDirty()
    {
      _isDirty = true;
    }

    public void Tick()
    {
      if (_saveDelay <= 0 && !_isSaving && _isDirty)
      {
        SaveExplicitAsync().Forget();
      }
    }

    public async UniTask SaveDirectAsync()
    {
      await UniTask.WaitWhile(() => _isSaving);
      await SaveExplicitAsync();
    }

    private async UniTask SaveExplicitAsync()
    {
      _isSaving = true;
      _isDirty = false;
      await _loader.SaveAsync();
      _saveDelay = _timers.Create(_config.SaveInterval);
      _isSaving = false;
    }
  }
}