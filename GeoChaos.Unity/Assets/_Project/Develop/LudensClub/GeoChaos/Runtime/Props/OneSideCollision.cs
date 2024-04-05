using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Props
{
  public struct OneSideCollision
  {
    public PackedCollider Sender;
    public Collider2D Other;

    public OneSideCollision(PackedCollider sender, Collider2D other)
    {
      Sender = sender;
      Other = other;
    }
  }
}