using System.Threading.Tasks;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Persistence
{
  public class GamePersistenceProcessor : IGamePersistenceProcessor, ITickable
  {
    private readonly IGameDataLoader _loader;
    private readonly ITimerFactory _timers;
    private readonly PersistenceConfig _config;
    private bool _isDirty;
    private Task _currentSave;
    private Timer _saveDelay = 0;

    public GamePersistenceProcessor(IGameDataLoader loader, ITimerFactory timers, IConfigProvider configProvider)
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
      if (_saveDelay <= 0)
      {
        if (_currentSave is { IsCompleted: true })
        {
          _currentSave = null;
          _saveDelay = _timers.Create(_config.SaveInterval);
        }

        if (_isDirty)
        {
          _isDirty = false;
          _currentSave = SaveImplicitAsync();
        }
      }
    }

    private async Task SaveImplicitAsync()
    {
      await _loader.SaveAsync();
    }
  }
}