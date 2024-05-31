using LudensClub.GeoChaos.Runtime.Constants;
using TMPro;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.UI
{
  [AddComponentMenu(ACC.Names.HEALTH_VIEW)]
  public class HealthView : MonoBehaviour
  {
    [SerializeField] private TMP_Text _text;

    public void SetText(string text)
    {
      _text.text = text;
    }
  }
}