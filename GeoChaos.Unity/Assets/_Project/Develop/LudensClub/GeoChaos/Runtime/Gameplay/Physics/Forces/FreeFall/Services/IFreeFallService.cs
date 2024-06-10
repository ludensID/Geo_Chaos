using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces
{
  public interface IFreeFallService
  {
    void StopFreeFall(EcsEntity owner, EcsEntities freeFalls);
    void PrepareFreeFall(EcsEntity freeFall, float time, float coefficient, bool useGradient);
  }
}