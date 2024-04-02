using System;
using System.Collections.Generic;
using System.Linq;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using TriInspector;
using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Debugging
{
  public class EcsEntityViewDebug : MonoBehaviour
  {
    public int Entity;
    
    [ShowInInspector]
    [ListDrawerSettings(Draggable = false, HideAddButton = true, HideRemoveButton = true, ShowElementLabels = true)]
    [OnValueChanged(nameof(OnComponentsChanged))]
    public List<IEcsComponent> Components;

    [ShowInInspector]
    [ListDrawerSettings(ShowElementLabels = true)]
    public List<IEcsComponent> UserComponents;
    
    private EcsWorld _world;
    
    [Inject]
    public void Construct(EcsWorld world)
    {
      _world = world;
    }

    public void OnComponentsChanged()
    {
      var types = new Type[_world.GetComponentsCount(Entity)];
      _world.GetComponentTypes(Entity, ref types);
      foreach (IEcsComponent component in Components)
      {
        var type = types.Single(x => component.GetType().IsAssignableFrom(x));
        _world.GetPoolByType(type).SetRaw(Entity, component);
      }
    }
  }
}