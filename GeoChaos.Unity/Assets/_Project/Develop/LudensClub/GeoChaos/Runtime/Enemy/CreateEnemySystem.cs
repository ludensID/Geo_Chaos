using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Characteristics.Components;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;
using LudensClub.GeoChaos.Runtime.Props.Enemy;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Enemy
{
  public class CreateEnemySystem : IEcsInitSystem
  {
    private readonly IEnemyFactory _factory;
    private readonly IGameObjectConverter _converter;
    private readonly EcsWorld _world;

    public CreateEnemySystem(GameWorldWrapper gameWorldWrapper, IEnemyFactory factory, IGameObjectConverter converter)
    {
      _factory = factory;
      _converter = converter;
      _world = gameWorldWrapper.World;
    }
    
    public void Init(EcsSystems systems)
    {
      var enemy = _world.NewEntity();
      _world.Add<Enemy>(enemy);
      _converter.Convert(_world, enemy, _factory.Create(new Vector3(5, 0, 0)));

      ref Health health = ref _world.Add<Health>(enemy);
      health.Value = 100;
    }
  }
}