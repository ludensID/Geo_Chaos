using Leopotam.EcsLite;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Collisions
{
  public class PackCollisionsSystem : IEcsRunSystem
  {
    private readonly ICollisionPacker _packer;

    public PackCollisionsSystem(ICollisionPacker packer)
    {
      _packer = packer;
    }

    public void Run(EcsSystems systems)
    {
      _packer.Pack();
    }
  }
}