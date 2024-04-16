using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Ring;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Hook
{
  public class CalculateHookSystem : IEcsRunSystem
  {
    private readonly ITimerFactory _timers;
    private readonly EcsWorld _game;
    private readonly EcsEntities _heroes;
    private readonly EcsEntities _selectedRings;
    private readonly HeroConfig _config;

    public CalculateHookSystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider, ITimerFactory timers)
    {
      _timers = timers;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _heroes = _game
        .Filter<OnHookStarted>()
        .Inc<ViewRef>()
        .Collect();

      _selectedRings = _game
        .Filter<RingTag>()
        .Inc<Selected>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity ring in _selectedRings)
      foreach (EcsEntity hero in _heroes)
      {
        Vector3 heroPosition = hero.Get<ViewRef>().View.transform.position;
        Vector3 targetPosition = ring.Get<RingPoints>().TargetPoint.position;

        float maxHeight = heroPosition.y > targetPosition.y
          ? heroPosition.y
          : targetPosition.y + _config.PullUpHeight;
        float heroDistance = maxHeight - heroPosition.y;
        if (heroDistance < 0)
          heroDistance = 0;
        float targetDistance = maxHeight - targetPosition.y;

        float startVelocityY = Mathf.Sqrt(2 * _config.PositiveGravity * heroDistance);
        float time = startVelocityY / _config.PositiveGravity
          + Mathf.Sqrt(2 * targetDistance / _config.PositiveFallGravity);
        float velocityX = Mathf.Abs(heroPosition.x - targetPosition.x) / time;

        hero.Replace((ref MovementVector x) =>
        {
          x.Speed = new Vector2(velocityX, startVelocityY);
          x.Direction.y = 1;
        });
        hero.Add((ref HookTimer timer) => timer.TimeLeft = _timers.Create(time));
      }
    }
  }
}