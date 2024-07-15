using System.Collections.Generic;
using System.Linq;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Converters
{
  [AddComponentMenu(ACC.Names.MONO_GAME_OBJECT_CONVERTER)]
  public class MonoGameObjectConverter : MonoBehaviour, IInitializable, IEcsConverter, IInjectable, IStartable
  {
    [SerializeField]
    private List<EcsConverterValue> _converters;

    private EcsWorld _message;
    private List<IEcsConverter> _cachedConverters;

    public bool Injected { get; set; }
    public bool Started { get; set; }


    [Inject]
    public void Construct(InitializableManager initializer, MessageWorldWrapper messageWorldWrapper)
    {
      if(!Started)
        initializer.Add(this);
      
      _message = messageWorldWrapper.World;
      _cachedConverters = GetComponents<IEcsConverter>().ToList();
      for (int i = 0; i < transform.childCount; i++)
      {
        GetConverters(transform.GetChild(i), _cachedConverters);
      }

      _cachedConverters.Remove(this);
      Injected = true;
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

#if UNITY_EDITOR
    private void Start()
    {
      Started = true;
      if (!this.EnsureInjection())
        Initialize();
    }
#endif
    
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