using System.Collections.Generic;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Collisions
{
  public interface ICollisionPacker
  {
    void Pack(bool isFixed = false);
    void PackImplicit(List<OneSideCollision> collisions);
  }
}