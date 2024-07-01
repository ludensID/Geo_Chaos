using LudensClub.GeoChaos.Runtime.Constants;
using TMPro;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.UI.HeroHealth
{
  [AddComponentMenu(ACC.Names.HERO_HEALTH_VIEW)]
  public class HeroHealthView : MonoBehaviour
  {
    [SerializeField]
    private TMP_Text _text;

    public void SetText(string text)
    {
      _text.text = text;
    }
  }
}