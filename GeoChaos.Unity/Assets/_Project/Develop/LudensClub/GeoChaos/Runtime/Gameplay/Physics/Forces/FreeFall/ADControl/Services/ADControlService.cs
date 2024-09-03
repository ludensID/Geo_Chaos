using System.Linq;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces
{
  public class ADControlService : IADControlService
  {
    private readonly PhysicsWorldWrapper _physicsWorldWrapper;
    private readonly EcsEntities _controls;
    private readonly BelongOwnerClosure _belongOwnerClosure = new BelongOwnerClosure();

    public ADControlService(PhysicsWorldWrapper physicsWorldWrapper)
    {
      _physicsWorldWrapper = physicsWorldWrapper;

      _controls = _physicsWorldWrapper.World
        .Filter<ADControl>()
        .Collect();
    }

    public EcsEntity GetADControl(EcsPackedEntity owner)
    {
      return GetLoop(owner).ToEnumerable().FirstOrDefault();
    }

    public EcsEntities GetLoop(EcsPackedEntity owner)
    {
      return _controls.Clone().Where(_belongOwnerClosure.SpecifyPredicate(owner));
    }
  }
}