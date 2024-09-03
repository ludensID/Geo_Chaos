using System.Linq;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces
{
  public class DragForceService : IDragForceService
  {
    private readonly PhysicsWorldWrapper _physicsWorldWrapper;
    private readonly EcsEntities _drags;
    private readonly BelongOwnerClosure _belongOwnerClosure = new BelongOwnerClosure();

    public DragForceService(PhysicsWorldWrapper physicsWorldWrapper)
    {
      _physicsWorldWrapper = physicsWorldWrapper;

      _drags = _physicsWorldWrapper.World
        .Filter<DragForce>()
        .Collect();
    }

    public EcsEntity GetDragForce(EcsPackedEntity owner)
    {
      return GetLoop(owner).ToEnumerable().FirstOrDefault();
    }

    public EcsEntities GetLoop(EcsPackedEntity owner)
    {
      return _drags.Clone().Where(_belongOwnerClosure.SpecifyPredicate(owner));
    }
  }
}