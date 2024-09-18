using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Windows.NothingHappens
{
  [AddComponentMenu(ACC.Names.NOTHING_HAPPENS_VIEW)]
  public class NothingHappensView : BaseWindowView
  {
    [SerializeField]
    private Button _button;

    private INothingHappensPresenter _presenter;

    [Inject]
    public void Construct(INothingHappensPresenter presenter)
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
      _presenter.CloseItself();
    }
  }
}