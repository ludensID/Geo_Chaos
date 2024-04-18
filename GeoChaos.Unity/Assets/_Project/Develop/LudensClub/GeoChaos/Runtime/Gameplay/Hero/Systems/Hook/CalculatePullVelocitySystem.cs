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
  public class CalculatePullVelocitySystem : IEcsRunSystem
  {
    private readonly ITimerFactory _timers;
    private readonly EcsWorld _game;
    private readonly EcsEntities _heroes;
    private readonly EcsEntities _selectedRings;
    private readonly HeroConfig _config;

    public CalculatePullVelocitySystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider, ITimerFactory timers)
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

        float maxHeight = Mathf.Max(heroPosition.y, targetPosition.y + _config.PullUpHeight);
        bool isDown = maxHeight == heroPosition.y;
        bool isHeroDelta = Mathf.Abs(maxHeight - heroPosition.y) < _config.VerticalHookHeroDelta;
        if (isDown && Mathf.Abs(maxHeight - targetPosition.y) < _config.VerticalHookTargetDelta)
          maxHeight += _config.PullDownHeight;
        float heroDistance = maxHeight - heroPosition.y;
        float targetDistance = maxHeight - targetPosition.y;

        float startVelocityY = Mathf.Sqrt(2 * _config.PositiveGravity * heroDistance);
        float pullTime = startVelocityY / _config.PositiveGravity;
        float fallTime = Mathf.Sqrt(2 * targetDistance / _config.PositiveFallGravity);
        float distance = Mathf.Abs(heroPosition.x - targetPosition.x);

        float time = pullTime + fallTime;
        float velocityX = distance / time;

        if (!isDown && !isHeroDelta)
        {
          Vector3 ringPosition = ring.Get<ViewRef>().View.transform.position;
          float pullDistance = Mathf.Abs(heroPosition.x - ringPosition.x);
          float fallDistance = Mathf.Abs(ringPosition.x - targetPosition.x);

          float alpha = fallTime * (pullTime + fallTime / 2);
          float startVelocityX = Mathf.Abs((pullTime * pullTime * fallDistance - 2 * alpha * pullDistance)
            / (pullTime * (pullTime * fallTime - 2 * alpha)));

          float accelerationX = (pullDistance - startVelocityX * pullTime) * 2 / (pullTime * pullTime);
          velocityX = startVelocityX;

          hero.Add((ref HookPulling pulling) =>
          {
            pulling.AccelerationX = accelerationX;
            pulling.VelocityX = startVelocityX;
          });
        }

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