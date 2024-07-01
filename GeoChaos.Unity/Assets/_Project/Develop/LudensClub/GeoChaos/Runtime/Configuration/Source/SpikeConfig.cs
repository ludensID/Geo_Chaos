using LudensClub.GeoChaos.Runtime.Constants;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Configuration
{
  [CreateAssetMenu(menuName = CAC.Names.SPIKE_MENU, fileName = CAC.Names.SPIKE_FILE)]
  public class SpikeConfig : ScriptableObject
  {
    public float Damage;
  }
}