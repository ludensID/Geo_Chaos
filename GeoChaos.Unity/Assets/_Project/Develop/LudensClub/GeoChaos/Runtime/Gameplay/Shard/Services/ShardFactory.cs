using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Props.Shard;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Shard
{
  public class ShardFactory : IShardFactory
  {
    private readonly GameWorldWrapper _gameWorldWrapper;
    private readonly ShardPool _pool;
    private readonly EcsEntity _createdShard;

    public ShardFactory(GameWorldWrapper gameWorldWrapper, ShardPool pool)
    {
      _gameWorldWrapper = gameWorldWrapper;
      _pool = pool;
      _createdShard = new EcsEntity(_gameWorldWrapper.World);
    }

    public EcsEntity Create(Vector3 position)
    {
      _createdShard.Entity = _gameWorldWrapper.World.NewEntity();
      ShardView view = _pool.Pop();
      view.transform.position = position;
      view.Converter.CreateEntity(_createdShard);
      return _createdShard;
    }
  }
}