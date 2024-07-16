using System.Collections.Generic;
using System.Linq;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Converters
{
  [RequireComponent(typeof(MonoInjector))]
  [AddComponentMenu(ACC.Names.GAME_OBJECT_CONVERTER)]
  public class GameObjectConverter : MonoBehaviour, IGameObjectConverter, IInitializable
  {
    [SerializeField]
    private List<EcsConverterValue> _converters;

    private EcsWorld _message;
    private List<IEcsConverter> _viewConverters;

    private IInitializingPhase _phase;
    private bool _initialized;

    public bool ShouldCreateEntity { get; set; } = true;

    [Inject]
    public void Construct(IInitializingPhase phase, InitializableManager initializer,
      MessageWorldWrapper messageWorldWrapper)
    {
      _phase = phase;
      if (!phase.WasInitialized)
        initializer.Add(this);
        
      _message = messageWorldWrapper.World;
      GetConvertersInChildren();
    }

    private void GetConvertersInChildren()
    {
      _viewConverters = GetComponents<IEcsConverter>().ToList();
      for (int i = 0; i < transform.childCount; i++)
      {
        GetConverters(transform.GetChild(i), _viewConverters);
      }

      _viewConverters.Remove(this);
    }

    public static void GetConverters(Transform t, List<IEcsConverter> converters)
    {
      IEcsConverter[] list = t.GetComponents<IEcsConverter>();
      if (list.Any(x => x is GameObjectConverter))
        return;

      converters.AddRange(list);
      for (int i = 0; i < t.childCount; i++)
      {
        GetConverters(t.GetChild(i), converters);
      }
    }

    private void Start()
    {
      if (_phase.WasInitialized && !_initialized)
        Initialize();
    }

    public void Initialize()
    {
      _initialized = true;
      if (ShouldCreateEntity)
      {
        _message.CreateEntity()
          .Add((ref CreateMonoEntityMessage message) => message.Converter = this);
      }
    }

    public void CreateEntity(EcsEntity entity)
    {
      foreach (EcsConverterValue converter in _converters)
        converter.ConvertTo(entity);

      ConvertTo(entity);
    }

    public void ConvertBackAndDestroy(EcsEntity entity)
    {
      ConvertBack(entity);
      Destroy(gameObject);
    }

    public void ConvertTo(EcsEntity entity)
    {
      foreach (IEcsConverter converter in _viewConverters)
        converter.ConvertTo(entity);

      entity.Add((ref ConverterRef converterRef) => converterRef.Converter = this);
    }

    public void ConvertBack(EcsEntity entity)
    {
      foreach (IEcsConverter converter in _viewConverters)
        converter.ConvertBack(entity);

      entity.Del<ConverterRef>();
    }
  }
}