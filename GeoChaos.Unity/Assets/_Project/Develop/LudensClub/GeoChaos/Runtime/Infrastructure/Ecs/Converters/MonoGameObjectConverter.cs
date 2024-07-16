using System.Collections.Generic;
using System.Linq;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Converters
{
  [RequireComponent(typeof(MonoInjector))]
  [AddComponentMenu(ACC.Names.MONO_GAME_OBJECT_CONVERTER)]
  public class MonoGameObjectConverter : MonoBehaviour, IInitializable, IEcsConverter
  {
    [SerializeField]
    private List<EcsConverterValue> _converters;

    private EcsWorld _message;
    private List<IEcsConverter> _cachedConverters;

    [Inject]
    public void Construct(IInitializingPhase phase, MessageWorldWrapper messageWorldWrapper)
    {
      _message = messageWorldWrapper.World;
      GetConvertersInChildren();

      phase.EnsureInitializing(this);
    }

    private void GetConvertersInChildren()
    {
      _cachedConverters = GetComponents<IEcsConverter>().ToList();
      for (int i = 0; i < transform.childCount; i++)
      {
        GetConverters(transform.GetChild(i), _cachedConverters);
      }

      _cachedConverters.Remove(this);
    }

    public static void GetConverters(Transform t, List<IEcsConverter> converters)
    {
      IEcsConverter[] list = t.GetComponents<IEcsConverter>();
      if (list.Any(x => x is MonoGameObjectConverter))
        return;

      converters.AddRange(list);
      for (int i = 0; i < t.childCount; i++)
      {
        GetConverters(t.GetChild(i), converters);
      }
    }

    public void Initialize()
    {
      _message.CreateEntity()
        .Add((ref CreateMonoEntityMessage message) => message.Converter = this);
    }

    public void Convert(EcsEntity entity)
    {
      foreach (EcsConverterValue converter in _converters)
        converter.Convert(entity);

      foreach (IEcsConverter converter in _cachedConverters)
        converter.Convert(entity);
    }
  }
}