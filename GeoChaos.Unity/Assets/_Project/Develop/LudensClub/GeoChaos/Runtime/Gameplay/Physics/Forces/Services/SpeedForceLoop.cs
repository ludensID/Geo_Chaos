using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces
{
  public class SpeedForceLoop
  {
    private readonly EcsEntities _entities;

    public SpeedForceLoop(EcsEntities entities)
    {
      _entities = entities;
    }

    public EcsEntities GetEntities()
    {
      return _entities;
    }

    public EcsEntities GetLoop(SpeedForceType type, EcsPackedEntity owner)
    {
      return _entities
        .Where<SpeedForce>(x => x.Type == type)
        .Where<Owner>(x => x.Entity.EqualsTo(owner));
    }
  }
}