using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Configuration
{
  [CreateAssetMenu(menuName = CAC.Names.INPUT_CONFIG_MENU, fileName = CAC.Names.INPUT_CONFIG_FILE)]
  public class InputConfig : ScriptableObject
  {
    public float OppositeHorizontalInputDelay;
  }
}