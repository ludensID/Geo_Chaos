using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Configuration
{
  [CreateAssetMenu(menuName = CAC.Names.PERSISTENCE_CONFIG_MENU, fileName = CAC.Names.PERSISTENCE_CONFIG_FILE)]
  public class PersistenceConfig : ScriptableObject
  {
    public float SaveInterval;
  }
}