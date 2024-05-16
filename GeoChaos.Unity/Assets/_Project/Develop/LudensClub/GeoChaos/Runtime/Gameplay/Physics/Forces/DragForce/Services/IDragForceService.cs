using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces
{
  public interface IDragForceService
  {
    EcsEntity GetDragForce(EcsPackedEntity owner);
    EcsEntities GetLoop(EcsPackedEntity owner);
  }
}