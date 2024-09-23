using Cysharp.Threading.Tasks;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine.EventSystems;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Windows.Simple
{
  public class SimpleWindowPresenter : ISimpleWindowPresenter, IInitializable
  {
    private readonly LevelStateMachine _levelStateMachine;
    private readonly EventSystem _eventSystem;
    private SimpleWindowView _view;

    public WindowType Id => _view.Id;
    public bool IsOpened { get; private set; }

    public SimpleWindowPresenter(LevelStateMachine levelStateMachine,
      IWindowManager windowManager,
      IExplicitInitializer initializer,
      EventSystem eventSystem)
    {
      _levelStateMachine = levelStateMachine;
      _eventSystem = eventSystem;
      windowManager.Add(this);
      initializer.Add(this);
    }

    public void Initialize()
    {
      _view.gameObject.SetActive(false);
      IsOpened = false;
    }

    public void SetView(SimpleWindowView view)
    {
      _view = view;
    }

    public void Open()
    {
      if (!IsOpened)
      {
        _levelStateMachine.SwitchState<PauseLevelState>().Forget();
        _view.gameObject.SetActive(true);
        IsOpened = true;
        _eventSystem.SetSelectedGameObject(_view.FirstNavigationElement.gameObject);
      }
    }

    public void Close()
    {
      if (IsOpened)
      {
        _view.gameObject.SetActive(false);
        _levelStateMachine.SwitchState<GameplayLevelState>().Forget();
        IsOpened = false;
        _eventSystem.SetSelectedGameObject(null);
      }
    }
  }
}