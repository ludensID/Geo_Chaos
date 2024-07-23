using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;
using LudensClub.GeoChaos.Runtime.Props.Shard;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Shard
{
  public class ShardFactory : IShardFactory
  {
    private readonly GameWorldWrapper _gameWorldWrapper;
    private readonly IShardPool _pool;
    private readonly IGameObjectConverterService _converter;

    public ShardFactory(GameWorldWrapper gameWorldWrapper, IShardPool pool, IGameObjectConverterService converter)
    {
      _gameWorldWrapper = gameWorldWrapper;
      _pool = pool;
      _converter = converter;
    }

    public EcsEntity Create()
    {
      EcsEntity instance = _gameWorldWrapper.World.CreateEntity()
        .Add<ShardTag>()
        .Add((ref EntityId id) => id.Id = EntityType.Shard)
        .Add<MovementVector>()
        .Add<ForceAvailable>()
        .Add<Poolable>();
      _converter.Convert(instance, _pool.Pull());
      return instance;
    }
  }
}