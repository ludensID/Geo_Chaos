using Leopotam.EcsLite;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces
{
  public class SpeedForceData
  {
    public SpeedForceType SpeedType;
    public EcsPackedEntity Owner;
    public Vector2 Impact;
    public Vector2 Speed;
    public Vector2 Direction;
    public bool Accelerated;
    public Vector2 Acceleration;
    public float MaxSpeed;
    public bool Instant;
    public bool Added;
    public bool Unique;
    public bool Immutable;

    public SpeedForceData(SpeedForceType type, EcsPackedEntity owner,  Vector2 impact = default(Vector2))
    {
      SpeedType = type;
      Owner = owner;
      Impact = impact;
    }
  }
}