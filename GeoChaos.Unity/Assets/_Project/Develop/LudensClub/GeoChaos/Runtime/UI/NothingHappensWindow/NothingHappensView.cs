using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.UI.NothingHappensWindow
{
  [AddComponentMenu(ACC.Names.NOTHING_HAPPENS_VIEW)]
  public class NothingHappensView : MonoBehaviour
  {
    [SerializeField]
    private Button _button;

    private INothingHappensPresenter _presenter;

    [Inject]
    public void Construct(INothingHappensPresenter presenter)
    {
      _presenter = presenter;
      _presenter.SetView(this);
      _button.onClick.AddListener(_presenter.CloseWindow);
    }
  }
}