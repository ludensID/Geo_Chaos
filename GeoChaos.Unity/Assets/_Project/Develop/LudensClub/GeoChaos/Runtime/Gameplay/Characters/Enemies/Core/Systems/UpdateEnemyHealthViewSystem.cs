using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characteristics.Health;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.UI;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies
{
  public class UpdateEnemyHealthViewSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _enemies;

    public UpdateEnemyHealthViewSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _enemies = _game
        .Filter<EnemyTag>()
        .Inc<HealthRef>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity enemy in _enemies)
      {
        HealthView healthView = enemy.Get<HealthRef>().View;
        SetPositionAndRotation(enemy, healthView.Canvas);
        SetText(healthView, enemy.Get<CurrentHealth>().Health);
      }
    }

    private static void SetText(HealthView healthView, float health)
    {
      healthView.SetText(health.ToString("###0"));
    }

    private static void SetPositionAndRotation(EcsEntity enemy, Canvas canvas)
    {
      Vector3 position = enemy.Get<ViewRef>().View.transform.position;
      Transform canvasTransform = canvas.transform;
      float distance = ((Vector2)(canvasTransform.position - position)).magnitude;
      canvasTransform.SetPositionAndRotation(position + Vector3.up * distance, Quaternion.identity);
    }
  }
}