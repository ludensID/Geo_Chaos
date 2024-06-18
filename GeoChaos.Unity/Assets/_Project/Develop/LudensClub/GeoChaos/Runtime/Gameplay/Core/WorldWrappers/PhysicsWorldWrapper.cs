using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Constants;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class PhysicsWorldWrapper : IEcsWorldWrapper
  {
    private readonly EcsWorld _world = new EcsWorld();
    
    public string Name => EcsConstants.Worlds.PHYSICS;
    public EcsWorld World => _world;
  }
}