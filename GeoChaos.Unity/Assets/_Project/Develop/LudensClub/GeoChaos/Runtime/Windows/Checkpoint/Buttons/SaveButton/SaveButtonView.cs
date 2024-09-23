using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Windows.Checkpoint
{
  [AddComponentMenu(ACC.Names.SAVE_BUTTON_VIEW)]
  public class SaveButtonView : MonoBehaviour
  {
    [SerializeField]
    private Button _button;

    private ISaveButtonPresenter _presenter;

    [Inject]
    public void Construct(ISaveButtonPresenter presenter)
    {
      _presenter = presenter;
      _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnDestroy()
    {
      _button.onClick.RemoveListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
      _presenter.SaveAsync().Forget();
    }

    private void Reset()
    {
      _button = GetComponentInChildren<Button>();
    }
  }
}