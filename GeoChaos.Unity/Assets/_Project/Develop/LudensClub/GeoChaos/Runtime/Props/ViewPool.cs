using System.Collections.Generic;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Props.Shard;
using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Props
{
  public abstract class ViewPool<TView> : IPool<TView>, IInitializable where TView : BaseView
  {
    private readonly PoolConfig _config;
    private readonly IViewFactory _factory;
    private readonly List<PooledView> _views = new List<PooledView>();

    private Transform _parent;

    public abstract EntityType Id { get; }

    public ViewPool(PoolConfig config, IViewFactory factory)
    {
      _config = config;
      _factory = factory;
    }

    public void Initialize()
    {
      _parent = new GameObject($"{typeof(TView).Name} Pool").transform;
      _parent.transform.position = Vector3.left * _config.DistanceFromOrigin;
      
      for (int i = 0; i < _config.InstanceCount; i++)
      {
        var instance = _factory.Create<TView>(Id);
        instance.Converter.ShouldCreateEntity = false;
        instance.transform.SetParent(_parent);
        instance.gameObject.SetActive(false);
        _views.Add(new PooledView(instance));
      }
    }

    public virtual bool HasId(EntityType id)
    {
      return id == Id;
    }

    public TView Pop()
    {
      PooledView pooledView = _views.Find(x => x.IsPooled);
      pooledView.IsPooled = false;

      pooledView.View.gameObject.SetActive(true);
      return pooledView.View;
    }

    public TView Pop(Vector3 position, Quaternion rotation, Transform parent = null)
    {
      PooledView pooledView = _views.Find(x => x.IsPooled);
      pooledView.IsPooled = false;
      
      TView instance = pooledView.View;
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

      PooledView pooledView = _views.Find(x => x.View == instance);
      pooledView.IsPooled = true;
    }

    private class PooledView
    {
      public readonly TView View;
      public bool IsPooled = true;

      public PooledView(TView view)
      {
        View = view;
      }
    }
  }
}