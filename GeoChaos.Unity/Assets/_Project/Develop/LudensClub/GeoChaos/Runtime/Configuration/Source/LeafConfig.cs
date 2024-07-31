using TriInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace LudensClub.GeoChaos.Runtime.Configuration
{
  [CreateAssetMenu(menuName = CAC.Names.LEAF_MENU, fileName = CAC.Names.LEAF_FILE)]
  public class LeafConfig : ScriptableObject
  {
    [FormerlySerializedAs("Speed")]
    [Title("Move")]
    public float MoveSpeed;
    public float Distance;
    [FormerlySerializedAs("SpeedCurve")]
    public AnimationCurve MoveSpeedCurve;

    [Title("Retraction")]
    public float RetractionSpeed;
  }
}