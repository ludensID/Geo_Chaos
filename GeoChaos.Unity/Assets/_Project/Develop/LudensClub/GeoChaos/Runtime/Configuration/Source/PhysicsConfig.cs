using LudensClub.GeoChaos.Runtime.Constants;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Configuration
{
  [CreateAssetMenu(fileName = CAC.PHYSICS_FILE, menuName = CAC.PHYSICS_MENU)]
  public class PhysicsConfig : ScriptableObject
  {
    public LayerMask GroundMask;
    public float AcceptableGroundDistance;
    public float GroundCheckTime;
  }
}