using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Core.Components;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;
using LudensClub.GeoChaos.Runtime.Props.Shard;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Shard
{
  public class ShardFactory : IShardFactory
  {
    private readonly GameWorldWrapper _gameWorldWrapper;
    private readonly IShardPool _pool;
    private readonly IGameObjectConverter _converter;

    public ShardFactory(GameWorldWrapper gameWorldWrapper, IShardPool pool, IGameObjectConverter converter)
    {
      _gameWorldWrapper = gameWorldWrapper;
      _pool = pool;
      _converter = converter;
    }

    public EcsEntity Create()
    {
      EcsEntity instance = _gameWorldWrapper.World.CreateEntity()
        .Add((ref EntityId id) => id.Id = EntityType.Shard)
        .Add<MovementVector>()
        .Add<ForceAvailable>()
        .Add<Poolable>();
      _converter.Convert(_gameWorldWrapper.World, instance.Entity, _pool.Pull());
      return instance;
    }
  }
}