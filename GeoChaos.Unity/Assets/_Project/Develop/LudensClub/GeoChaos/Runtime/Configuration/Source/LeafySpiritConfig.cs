using TriInspector;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Configuration
{
  [CreateAssetMenu(menuName = CAC.Names.LEAFY_SPIRIT_MENU, fileName = CAC.Names.LEAFY_SPIRIT_FILE)]
  public class LeafySpiritConfig : ScriptableObject
  {
    [Title("Wait")]
    public float WaitingTime;

    [Title("Leap")]
    public float PrecastLeapTime;
    public float LeapTime;
    public float MinLeapDistance;

    [Title("Detection")]
    public float MaxVerticalDistance;

    [Title("Rising")]
    public float RisingTime;
  }
}