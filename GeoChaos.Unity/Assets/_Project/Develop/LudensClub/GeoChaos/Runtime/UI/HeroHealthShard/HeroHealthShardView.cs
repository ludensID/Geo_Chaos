using TMPro;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.UI.HeroHealthShard
{
  [AddComponentMenu(ACC.Names.HERO_HEALTH_SHARD_VIEW)]
  public class HeroHealthShardView : MonoBehaviour
  {
    [SerializeField]
    private TMP_Text _text;

    public void SetText(string text)
    {
      _text.text = text;
    }
  }
}