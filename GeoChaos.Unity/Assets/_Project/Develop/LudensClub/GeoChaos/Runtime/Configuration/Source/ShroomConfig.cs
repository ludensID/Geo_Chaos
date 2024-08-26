using TriInspector;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Configuration
{
  [CreateAssetMenu(menuName = CAC.Names.SHROOM_CONFIG_MENU, fileName = CAC.Names.SHROOM_CONFIG_FILE)]
  public class ShroomConfig : ScriptableObject
  {
    [Title("Attack")]
    public float ReloadingTime;
  }
}