#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;
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
    [OnValueChanged(TriConstants.ON + nameof(Components) + TriConstants.CHANGED)]
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
      foreach (var component in Components)
      {
        var type = types.Single(x => component.GetType().IsAssignableFrom(x));
        _world.GetPoolByType(type).SetRaw(Entity, component);
      }
    }
  }
}
#endif