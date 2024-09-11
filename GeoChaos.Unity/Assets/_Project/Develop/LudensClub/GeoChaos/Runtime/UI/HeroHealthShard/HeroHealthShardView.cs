using TMPro;
using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.UI.HeroHealthShard
{
  [AddComponentMenu(ACC.Names.HERO_HEALTH_SHARD_VIEW)]
  public class HeroHealthShardView : MonoBehaviour
  {
    [SerializeField]
    private TMP_Text _text;

    [Inject]
    public void Construct(IHeroHealthShardPresenter presenter)
    {
      presenter.SetView(this);
    }
    
    public void SetText(string text)
    {
      _text.text = text;
    }
  }
}