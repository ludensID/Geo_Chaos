using TMPro;
using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.UI
{
  [AddComponentMenu(ACC.Names.DASH_COOLDOWN_VIEW)]
  public class DashCooldownView : MonoBehaviour
  {
    [SerializeField]
    private TMP_Text _text;

    [Inject]
    public void Construct(IDashCooldownPresenter presenter)
    {
      presenter.SetView(this);
    }

    public void SetText(string text)
    {
      _text.text = text;
    }
  }
}