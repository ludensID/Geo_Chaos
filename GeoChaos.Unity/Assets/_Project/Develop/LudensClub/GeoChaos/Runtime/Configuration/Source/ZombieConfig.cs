using TriInspector;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Configuration
{
  [CreateAssetMenu(menuName = CAC.Names.ZOMBIE_CONFIG_MENU, fileName = CAC.Names.ZOMBIE_CONFIG_FILE)]
  public class ZombieConfig : ScriptableObject
  {
    [Title("Wait")]
    public float WaitTime;

    [Title("Move")]
    public float CalmSpeed;

    [Title("Attack")]
    public float BackDistance;

    [Title("Attack With Arms")]
    public float ArmsTime;

    public float ArmsCooldown;
    public float AttackTime;
    public float AttackSpeed;
    public float AttackCooldown;

    [Title("Watch")]
    public float WatchTime;
  }
}