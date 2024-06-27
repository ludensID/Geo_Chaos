using TMPro;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.UI.HeroHealth
{
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