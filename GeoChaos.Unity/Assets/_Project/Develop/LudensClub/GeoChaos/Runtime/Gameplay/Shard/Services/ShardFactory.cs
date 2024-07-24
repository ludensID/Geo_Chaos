using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Props.Shard;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Shard
{
  public class ShardFactory : IShardFactory
  {
    private readonly GameWorldWrapper _gameWorldWrapper;
    private readonly IShardPool _pool;
    private readonly EcsEntity _createdShard;

    public ShardFactory(GameWorldWrapper gameWorldWrapper, IShardPool pool)
    {
      _gameWorldWrapper = gameWorldWrapper;
      _pool = pool;
      _createdShard = new EcsEntity(_gameWorldWrapper.World);
    }

    public EcsEntity Create()
    {
      _createdShard.Entity = _gameWorldWrapper.World.NewEntity();
      ShardView view = _pool.Pull();
      view.Converter.CreateEntity(_createdShard);
      return _createdShard;
    }
  }
}