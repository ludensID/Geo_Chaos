using Leopotam.EcsLite;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Collisions
{
  public class PackFixedCollisionsSystem : IEcsRunSystem
  {
    private readonly ICollisionPacker _packer;

    public PackFixedCollisionsSystem(ICollisionPacker packer)
    {
      _packer = packer;
    }

    public void Run(EcsSystems systems)
    {
      _packer.Pack(true);
    }
  }
}