using LudensClub.GeoChaos.Runtime.Constants;
using TMPro;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.UI.ImmunityDuration
{
  [AddComponentMenu(ACC.Names.IMMUNITY_DURATION_VIEW)]
  public class ImmunityDurationView : MonoBehaviour
  {
    [SerializeField]
    private TMP_Text _text;
    
    public void SetText(string text)
    {
      _text.text = text;
    }
  }
}