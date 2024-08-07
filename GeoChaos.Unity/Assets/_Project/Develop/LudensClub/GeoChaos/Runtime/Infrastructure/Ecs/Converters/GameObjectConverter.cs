﻿using System.Collections.Generic;
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

    [SerializeField]
    private bool _createEntityOnStart = true;

    [SerializeField]
    private bool _createEntityOnEnable;

    private EcsWorld _message;
    private List<IEcsConverter> _viewConverters;

    private IInitializingPhase _phase;
    private bool _initialized;
    private EcsEntity _entity = new EcsEntity();

    public bool ShouldCreateEntity { get; set; } = true;
    public EcsEntity Entity => _entity;

    public bool CreateEntityOnStart
    {
      get => _createEntityOnStart;
      set => _createEntityOnStart = value;
    }

    public bool CreateEntityOnEnable
    {
      get => _createEntityOnEnable;
      set => _createEntityOnEnable = value;
    }

    [Inject]
    public void Construct(IInitializingPhase phase, MessageWorldWrapper messageWorldWrapper)
    {
      _phase = phase;
      phase.Add(this);

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

    private void OnEnable()
    {
      if(CreateEntityOnEnable)
        SendCreateMessage();
    }

    private void Start()
    {
      if (_phase.WasInitialized && !_initialized)
        Initialize();
    }

    public void Initialize()
    {
      if (!isActiveAndEnabled)
        return;

      _initialized = true;
      if (ShouldCreateEntity && CreateEntityOnStart)
        SendCreateMessage();
    }

    public void SendCreateMessage()
    {
      _message.CreateEntity()
        .Add((ref CreateEntityMessage createMessage) =>
        {
          createMessage.Entity = _entity.PackedEntity;
          createMessage.Converter = this;
        });
    }

    public void CreateEntity()
    {
      foreach (EcsConverterValue converter in _converters)
        converter.ConvertTo(_entity);

      ConvertTo(_entity);
    }

    public void CreateEntity(EcsEntity entity)
    {
      _entity.Copy(entity);
      CreateEntity();
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
    
    public void SetEntity(EcsEntity entity)
    {
      _entity.Copy(entity);
    }

    public void SetEntity(EcsWorld world, int entity)
    {
      _entity.SetWorld(world, entity);
    }
  }
}