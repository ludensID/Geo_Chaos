using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Configuration
{
  [CreateAssetMenu(fileName = CAC.HERO_FILE, menuName = CAC.HERO_MENU)]
  public class HeroConfig : ScriptableObject
  {
    public float MovementSpeed;
    public float MovementResponseDelay;
    public float AccelerationTime;
    public float JumpForce;
    public float ForcedStopInertia;
    public float VelocityDropToStop;

    public float VelocityToStop => JumpForce - VelocityDropToStop;
  }
}