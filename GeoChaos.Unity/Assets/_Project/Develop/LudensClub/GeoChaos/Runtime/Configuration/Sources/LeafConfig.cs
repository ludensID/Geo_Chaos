using TriInspector;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Configuration
{
  [CreateAssetMenu(menuName = CAC.Names.LEAF_MENU, fileName = CAC.Names.LEAF_FILE)]
  public class LeafConfig : ScriptableObject
  {
    [Title("Move")]
    public float MoveSpeed;
    public float Distance;
    public AnimationCurve MoveSpeedCurve;

    [Title("Retraction")]
    public float RetractionSpeed;
  }
}