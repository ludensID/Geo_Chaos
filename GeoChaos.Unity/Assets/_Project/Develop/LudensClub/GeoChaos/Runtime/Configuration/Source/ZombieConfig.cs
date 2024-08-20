using TriInspector;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Configuration
{
  [CreateAssetMenu(menuName = CAC.Names.ZOMBIE_CONFIG_MENU, fileName = CAC.Names.ZOMBIE_CONFIG_FILE)]
  public class ZombieConfig : ScriptableObject
  {
    [Title("Wait")]
    public float WaitTime;
  }
}