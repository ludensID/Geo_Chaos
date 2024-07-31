using System.Linq;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

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

    public EcsEntity GetForce(SpeedForceType type, EcsPackedEntity owner)
    {
      return GetLoop(type, owner).ToEnumerable().FirstOrDefault();
    }

    public EcsEntities GetLoop(SpeedForceType type, EcsPackedEntity owner)
    {
      return _entities.Clone()
        .Where<SpeedForce>(x => x.Type == type)
        .Where<Owner>(x => x.Entity.EqualsTo(owner));
    }
  }
}