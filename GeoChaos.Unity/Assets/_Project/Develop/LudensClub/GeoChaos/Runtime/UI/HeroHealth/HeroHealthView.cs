using TMPro;
using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.UI.HeroHealth
{
  [AddComponentMenu(ACC.Names.HERO_HEALTH_VIEW)]
  public class HeroHealthView : MonoBehaviour
  {
    [SerializeField]
    private TMP_Text _text;

    [Inject]
    public void Construct(IHeroHealthPresenter presenter)
    {
      presenter.SetView(this);
    }
      
    public void SetText(string text)
    {
      _text.text = text;
    }
  }
}