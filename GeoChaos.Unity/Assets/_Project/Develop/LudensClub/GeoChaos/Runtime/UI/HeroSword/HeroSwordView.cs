using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.UI
{
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