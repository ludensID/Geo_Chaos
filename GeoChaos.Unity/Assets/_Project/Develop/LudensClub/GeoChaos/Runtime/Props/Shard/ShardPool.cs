using System.Collections.Generic;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Props.Shard
{
  public class ShardPool : IShardPool, IInitializable
  {
    private readonly IViewFactory _factory;
    private readonly ShardPoolConfig _config;
    private readonly List<PooledShard> _shards = new List<PooledShard>();

    private Transform _parent;

    public ShardPool(IConfigProvider configProvider, IViewFactory factory)
    {
      _factory = factory;
      _config = configProvider.Get<ShardPoolConfig>();
    }

    public void Initialize()
    {
      _parent = new GameObject("Shard Pool").transform;
      _parent.transform.position = Vector3.left * _config.DistanceFromOrigin;
      
      for (int i = 0; i < _config.InstanceCount; i++)
      {
        var instance = _factory.Create<ShardView>(EntityType.Shard);
        instance.Converter.ShouldCreateEntity = false;
        instance.transform.SetParent(_parent);
        instance.gameObject.SetActive(false);
        _shards.Add(new PooledShard(instance));
      }
    }

    public bool HasId(EntityType id)
    {
      return id == EntityType.Shard;
    }

    public ShardView Pull()
    {
      PooledShard pooledShard = _shards.Find(x => x.IsPooled);
      pooledShard.IsPooled = false;

      pooledShard.Shard.gameObject.SetActive(true);
      return pooledShard.Shard;
    }

    public ShardView Pull(Vector3 position, Quaternion rotation, Transform parent = null)
    {
      PooledShard pooledShard = _shards.Find(x => x.IsPooled);
      pooledShard.IsPooled = false;
      
      ShardView instance = pooledShard.Shard;
      instance.transform.SetParent(parent);
      instance.transform.SetPositionAndRotation(position, rotation);
      instance.gameObject.SetActive(true);
      return instance;
    }

    public void Push(BaseView instance)
    {
      instance.gameObject.SetActive(false);
      instance.transform.SetParent(_parent);
      instance.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);

      PooledShard pooledShard = _shards.Find(x => x.Shard == instance);
      pooledShard.IsPooled = true;
    }

    private class PooledShard
    {
      public readonly ShardView Shard;
      public bool IsPooled = true;

      public PooledShard(ShardView shard)
      {
        Shard = shard;
      }
    }
  }
}