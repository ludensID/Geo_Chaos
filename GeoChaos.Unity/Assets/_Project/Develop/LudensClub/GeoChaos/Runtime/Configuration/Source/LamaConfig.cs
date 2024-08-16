using TriInspector;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Configuration
{
  [CreateAssetMenu(menuName = CAC.Names.LAMA_MENU, fileName = CAC.Names.LAMA_FILE)]
  public class LamaConfig : ScriptableObject
  {
    public float MovementSpeed;
    
    [Title("Patrol")]
    public Vector2 PatrolStep;
    public float LookingTime;
    
    [Title("Chase")]
    public float ViewRadius;
    public float ListenTime;
    
    [Title("Attack")]
    public float AttackDistance;
    public float HitTime;
    public float BiteTime;
    public float HitCooldown;
    public float ComboCooldown;
    public float HitDamage;
    public float BiteDamage;
    public float DamageFromBody;
  }
}