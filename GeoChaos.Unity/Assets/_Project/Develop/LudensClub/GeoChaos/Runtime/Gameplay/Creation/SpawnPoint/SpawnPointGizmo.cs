using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Creation.SpawnPoint
{
  [AddComponentMenu(ACC.Names.SPAWN_GIZMO)]
  public class SpawnPointGizmo : MonoBehaviour
  {
#if UNITY_EDITOR
    private GameObjectConverter _converter;
    private void OnDrawGizmos()
    {
      if (!_converter)
        _converter = GetComponent<GameObjectConverter>();
      if (!_converter)
        return;
      var converters = (List<EcsConverterValue>)_converter.GetType()
        .GetField("_converters", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(_converter);
      if (converters == null)
        return;
      
      EntityType id = converters.Where(x => x.ShowComponents)
        .Select(x => (x.GetValue() as EcsComponentsConverter)?.Components.Select(y => y.Value)
          .OfType<SpawnedEntityId>()
          .FirstOrDefault()
          .Id ?? EntityType.None).FirstOrDefault();

      if (id == EntityType.None)
        return;
      
      Color color = id == EntityType.Hero ? Color.green : Color.red;
      color.a = 0.5f;
      Gizmos.color = color;
      Gizmos.DrawSphere(transform.position, 0.5f);
    }
#endif
  }
}