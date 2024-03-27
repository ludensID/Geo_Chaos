using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Props;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class CreatePlayerSystem : IEcsInitSystem
  {
    private readonly IPlayerFactory _factory;
    private readonly EcsWorld _world;
    private readonly PlayerConfig _config;

    public CreatePlayerSystem(IPlayerFactory factory, IEcsWorldProvider provider, IConfigProvider config)
    {
      _factory = factory;
      _world = provider.World;
      _config = config.Get<PlayerConfig>();
    }
    
    public void Init(EcsSystems systems)
    {
      int entity = _world.NewEntity();
      _world.CreateView(entity, () => _factory.Create(Vector3.zero));
    }
  }
}