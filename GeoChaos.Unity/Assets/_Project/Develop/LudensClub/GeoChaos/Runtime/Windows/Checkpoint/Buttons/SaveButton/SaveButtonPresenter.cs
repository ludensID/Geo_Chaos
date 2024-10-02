using Cysharp.Threading.Tasks;
using LudensClub.GeoChaos.Runtime.Persistence;
using LudensClub.GeoChaos.Runtime.Windows.Map;

namespace LudensClub.GeoChaos.Runtime.Windows.Checkpoint
{
  public class SaveButtonPresenter : ISaveButtonPresenter
  {
    private readonly IPersistenceService _persistence;
    private readonly MapModel _mapModel;
    private readonly IWindowManager _windowManager;

    public SaveButtonPresenter(IPersistenceService persistence, MapModel mapModel, IWindowManager windowManager)
    {
      _persistence = persistence;
      _mapModel = mapModel;
      _windowManager = windowManager;
    }

    public async UniTask SaveAsync()
    {
      if (_mapModel.CurrentCheckpoint.IsAlive())
      {
        _persistence.GamePersistence.LastCheckpoint = _mapModel.CurrentCheckpoint.Get<PersistenceIdRef>().Identifier.Id;
      }
      
      await _persistence.SaveGameDirect();
      _windowManager.OpenAsNew(WindowType.Save);
    }
  }
}