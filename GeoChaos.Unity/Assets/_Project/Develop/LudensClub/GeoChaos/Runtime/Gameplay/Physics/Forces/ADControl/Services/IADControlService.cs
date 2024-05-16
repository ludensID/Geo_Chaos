using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces
{
  public interface IADControlService
  {
    EcsEntity GetADControl(EcsPackedEntity owner);
    EcsEntities GetLoop(EcsPackedEntity owner);
  }
}