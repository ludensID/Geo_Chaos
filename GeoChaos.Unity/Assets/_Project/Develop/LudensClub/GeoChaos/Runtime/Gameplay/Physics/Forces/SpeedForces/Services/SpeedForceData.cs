using Leopotam.EcsLite;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces
{
  public struct SpeedForceData
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
    public bool Draggable;
    public bool Valuable;
    public bool Residual;
    public bool Spare;

    public SpeedForceData(SpeedForceType type, EcsPackedEntity owner,  Vector2 impact = default(Vector2))
    {
      SpeedType = type;
      Owner = owner;
      Impact = impact;
      Speed = Vector2.zero; 
      Direction = Vector2.zero;
      Accelerated = false;
      Acceleration = Vector2.zero;
      MaxSpeed = 0;
      Instant = false;
      Added = false;
      Unique = false;
      Immutable = false;
      Draggable = false;
      Valuable = false;
      Residual = false;
      Spare = false;
    }
  }
}