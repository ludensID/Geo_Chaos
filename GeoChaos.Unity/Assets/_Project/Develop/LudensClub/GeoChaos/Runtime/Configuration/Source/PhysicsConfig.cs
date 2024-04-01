using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Configuration
{
  [CreateAssetMenu(fileName = CAC.PHYSICS_FILE, menuName = CAC.PHYSICS_MENU)]
  public class PhysicsConfig : ScriptableObject
  {
    public LayerMask GroundLayer;
    public float AcceptableGroundDistance;
  }
}