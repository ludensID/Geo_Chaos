using LudensClub.GeoChaos.Runtime.Constants;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.UI
{
  [AddComponentMenu(ACC.Names.HERO_SWORD_VIEW)]
  public class HeroSwordView : MonoBehaviour
  {
    public Color DefaultColor = Color.white;
    public Color AttackColor = Color.green;
    public Color ComboColor = Color.yellow;

    [SerializeField]
    private SpriteRenderer _sword;

    public void SetColor(Color color)
    {
      _sword.color = color;
    }
  }
}