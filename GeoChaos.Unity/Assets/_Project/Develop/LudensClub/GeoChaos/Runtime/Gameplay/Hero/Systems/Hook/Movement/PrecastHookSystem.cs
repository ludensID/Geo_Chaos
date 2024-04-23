using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Ring;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Hook
{
  public class PrecastHookSystem : IEcsRunSystem
  {
    private readonly ITimerFactory _timers;
    private readonly EcsWorld _game;
    private readonly EcsEntities _commands;
    private readonly EcsEntities _hookedRings;
    private readonly HeroConfig _config;

    public PrecastHookSystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider, ITimerFactory timers)
    {
      _timers = timers;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _commands = _game
        .Filter<HookCommand>()
        .Inc<ViewRef>()
        .Collect();

      _hookedRings = _game
        .Filter<RingTag>()
        .Inc<Hooked>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity ring in _hookedRings)
      foreach (EcsEntity command in _commands)
      {
        Vector3 heroPosition = command.Get<ViewRef>().View.transform.position;
        Vector3 ringPosition = ring.Get<ViewRef>().View.transform.position;

        Vector3 distance = ringPosition - heroPosition;
        command
          .Add<OnHookPrecastStarted>()
          .Add((ref HookPrecast precast) =>
        {
          precast.TimeLeft = _timers.Create(_config.HookPrecastTime);
          precast.Velocity = distance / _config.HookPrecastTime;
          precast.TargetPoint = ringPosition;
        });
      }
    }
  }
}