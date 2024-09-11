using TMPro;
using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.UI
{
  [AddComponentMenu(ACC.Names.SHOOT_COOLDOWN_VIEW)]
  public class ShootCooldownView : MonoBehaviour
  {
    [SerializeField]
    private TMP_Text _text;

    [Inject]
    public void Construct(IShootCooldownPresenter presenter)  
    {
      presenter.SetView(this);
    }

    public void SetText(string text)
    {
      _text.text = text;
    }
  }
}