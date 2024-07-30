using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Configuration
{
  [CreateAssetMenu(menuName = CAC.Names.LEAF_MENU, fileName = CAC.Names.LEAF_FILE)]
  public class LeafConfig : ScriptableObject
  {
    public float Speed;
    public float Distance;
  }
}