using TMPro;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.UI
{
  public class HealthView : MonoBehaviour
  {
    [SerializeField] private TMP_Text _text;

    public void SetText(string text)
    {
      _text.text = text;
    }
  }
}