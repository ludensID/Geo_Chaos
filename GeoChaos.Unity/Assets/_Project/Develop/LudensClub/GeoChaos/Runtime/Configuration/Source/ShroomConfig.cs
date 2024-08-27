using TriInspector;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Configuration
{
  [CreateAssetMenu(menuName = CAC.Names.SHROOM_MENU, fileName = CAC.Names.SHROOM_FILE)]
  public class ShroomConfig : ScriptableObject
  {
    [Title("Attack")]
    public float ReloadingTime;
  }
}