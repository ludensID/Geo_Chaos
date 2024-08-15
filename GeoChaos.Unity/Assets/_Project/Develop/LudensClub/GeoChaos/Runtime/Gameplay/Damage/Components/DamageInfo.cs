using Leopotam.EcsLite;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Damage
{
  public struct DamageInfo
  {
    public EcsPackedEntity Master;
    public EcsPackedEntity Target;
    public float Damage;
    public Vector3 BumpPosition;

    public DamageInfo(EcsPackedEntity master, EcsPackedEntity target, float damage, Vector3 bumpPosition)
    {
      Master = master;
      Target = target;
      Damage = damage;
      BumpPosition = bumpPosition;
    }
  }
}