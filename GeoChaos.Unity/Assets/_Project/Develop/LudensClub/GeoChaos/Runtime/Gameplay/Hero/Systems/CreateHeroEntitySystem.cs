using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Characteristics.Components;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;
using LudensClub.GeoChaos.Runtime.Props;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class CreateHeroEntitySystem : IEcsInitSystem
  {
    private readonly IHeroFactory _factory;
    private readonly IGameObjectConverter _converter;
    private readonly EcsWorld _world;

    public CreateHeroEntitySystem(IHeroFactory factory,
      GameWorldWrapper worldWrapper,
      IGameObjectConverter converter)
    {
      _factory = factory;
      _converter = converter;
      _world = worldWrapper.World;
    }

    public void Init(EcsSystems systems)
    {
      int hero = _world.NewEntity();
      _world.Add<Hero>(hero);
      _converter.Convert(_world, hero, _factory.Create(Vector3.zero));

      ref Health health = ref _world.Add<Health>(hero);
      health.Value = 100;
    }
  }
}