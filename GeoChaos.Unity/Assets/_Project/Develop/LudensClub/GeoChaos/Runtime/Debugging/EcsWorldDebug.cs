using System.Collections.Generic;
using System.Linq;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Debugging
{
  public class EcsWorldDebug : MonoBehaviour, IEcsWorldEventListener
  {
    private readonly List<EcsEntityViewDebug> _entities = new List<EcsEntityViewDebug>();
    private EcsWorld _world;
    private EcsEntityViewDebugFactory _factory;

    [Inject]
    public void Construct(IWorldWrapper worldWrapper, EcsEntityViewDebugFactory factory)
    {
      _factory = factory;
      _world = worldWrapper.World;
      _world.AddEventListener(this);
    }

    private void Update()
    {
      foreach (EcsEntityViewDebug entity in _entities)
      {
        var components = new object[_world.GetComponentsCount(entity.Entity)];
        _world.GetComponents(entity.Entity, ref components);
        entity.Components = components.Cast<IEcsComponent>().ToList();
        EditorUtility.SetDirty(entity);
      }
    }

    public void OnEntityCreated(int entity)
    {
      _entities.Add(_factory.Create(_world, entity, transform));
    }

    public void OnEntityChanged(int entity)
    {
    }

    public void OnEntityDestroyed(int entity)
    {
      EcsEntityViewDebug entityView = _entities.Find(x => x.Entity == entity);
      _entities.Remove(entityView);
    }

    public void OnFilterCreated(EcsFilter filter)
    {
    }

    public void OnWorldResized(int newSize)
    {
    }

    public void OnWorldDestroyed(EcsWorld world)
    {
      _world.RemoveEventListener(this);
    }
  }
}