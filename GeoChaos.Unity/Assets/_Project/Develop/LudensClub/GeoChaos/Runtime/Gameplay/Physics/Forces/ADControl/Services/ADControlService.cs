﻿using System.Linq;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces
{
  public class ADControlService : IADControlService
  {
    private readonly PhysicsWorldWrapper _physicsWorldWrapper;
    private readonly EcsEntities _controls;

    public ADControlService(PhysicsWorldWrapper physicsWorldWrapper)
    {
      _physicsWorldWrapper = physicsWorldWrapper;

      _controls = _physicsWorldWrapper.World
        .Filter<ADControl>()
        .Collect();
    }

    public EcsEntity GetADControl(EcsPackedEntity owner)
    {
      return GetLoop(owner).FirstOrDefault();
    }

    public EcsEntities GetLoop(EcsPackedEntity owner)
    {
      return _controls.Where<Owner>(x => x.Entity.EqualsTo(owner));
    }
  }
}