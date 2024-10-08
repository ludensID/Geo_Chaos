﻿using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Glide;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Gravity;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Dash
{
  public class StopHeroDashSystem : IEcsRunSystem
  {
    private readonly ISpeedForceFactory _forceFactory;
    private readonly ITimerFactory _timers;
    private readonly EcsWorld _game;
    private readonly HeroConfig _config;
    private readonly EcsEntities _commands;

    public StopHeroDashSystem(GameWorldWrapper gameWorldWrapper,
      ISpeedForceFactory forceFactory,
      ITimerFactory timers,
      IConfigProvider configProvider)
    {
      _forceFactory = forceFactory;
      _timers = timers;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _commands = _game
        .Filter<HeroTag>()
        .Inc<StopDashCommand>()
        .Inc<Dashing>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity command in _commands)
      {
        _forceFactory.Create(new SpeedForceData(SpeedForceType.Dash, command.PackedEntity, Vector2.right)
        {
          Speed = Vector2.zero,
          Instant = true
        });
        
        command
          .Del<Dashing>()
          .Add((ref DashCooldown cooldown) => cooldown.TimeLeft = _timers.Create(_config.DashCooldown))
          .Change((ref GravityScale gravity) => gravity.Enabled.Value = true)
          .Replace((ref LastGlideMovement glide) => glide.Movement = MovementType.Dash); 
          
        ref MovementLayout layout = ref command.Get<MovementLayout>();
        if (layout.Movement == MovementType.Dash)
        {
          layout.Layer = MovementLayer.All;
          layout.Movement = MovementType.None;
        }
      }
    }
  }
}