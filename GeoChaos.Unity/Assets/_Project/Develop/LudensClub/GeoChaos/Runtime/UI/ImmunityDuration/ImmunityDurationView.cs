using TMPro;
using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.UI.ImmunityDuration
{
  [AddComponentMenu(ACC.Names.IMMUNITY_DURATION_VIEW)]
  public class ImmunityDurationView : MonoBehaviour
  {
    [SerializeField]
    private TMP_Text _text;

    [Inject]
    public void Construct(IImmunityDurationPresenter presenter)
    {
      presenter.SetView(this);
    }
    
    public void SetText(string text)
    {
      _text.text = text;
    }
  }
}