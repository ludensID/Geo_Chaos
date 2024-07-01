using System.Collections.Generic;
using System.Linq;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Converters
{
  public class MonoGameObjectConverter : MonoBehaviour, IEcsConverter, IInjectable
  {
    public EntityType Id;

    private EcsWorld _message;
    private List<IEcsConverter> _converters;
    
    public bool Injected { get; set; }


    [Inject]
    public void Construct(GameWorldWrapper gameWorldWrapper, MessageWorldWrapper messageWorldWrapper)
    {
      _message = messageWorldWrapper.World;
      _converters = GetComponents<IEcsConverter>().ToList();
      _converters.Remove(this);
      Injected = true;
    }

    private void Start()
    {
#if UNITY_EDITOR
      this.EnsureInjection();
#endif
      _message.CreateEntity()
        .Add((ref CreateMonoEntityMessage message) => message.Converter = this);
    }

    public void Convert(EcsEntity entity)
    {
      foreach (IEcsConverter converter in _converters)
        converter.Convert(entity);
    }
  }
}