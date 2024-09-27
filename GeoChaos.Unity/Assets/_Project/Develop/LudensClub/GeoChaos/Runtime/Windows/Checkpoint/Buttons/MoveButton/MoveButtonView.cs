using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Windows.Checkpoint
{
  [AddComponentMenu(ACC.Names.MOVE_BUTTON_VIEW)]
  public class MoveButtonView : MonoBehaviour
  {
    [SerializeField]
    private Button _button;

    private IMoveButtonPresenter _presenter;

    [Inject]
    public void Construct(IMoveButtonPresenter presenter)
    {
      _presenter = presenter;
      _presenter.SetView(this);
      _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnDestroy()
    {
      _button.onClick.RemoveListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
      _presenter.OpenMap();
    }

    public void SetActiveButton(bool active)
    {
      _button.interactable = active;
    }

    private void Reset()
    {
      _button = GetComponentInChildren<Button>();
    }
  }
}