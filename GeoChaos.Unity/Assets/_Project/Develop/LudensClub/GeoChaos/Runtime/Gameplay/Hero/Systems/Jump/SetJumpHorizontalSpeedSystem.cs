﻿using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class SetJumpHorizontalSpeedSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly HeroConfig _config;
    private readonly EcsFilter _onGrounds;
    private readonly EcsFilter _onNotGrounds;

    public SetJumpHorizontalSpeedSystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider)
    {
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _onGrounds = _game
        .Filter<HorizontalSpeed>()
        .Inc<OnGround>()
        .End();

      _onNotGrounds = _game
        .Filter<HorizontalSpeed>()
        .Inc<OnNotGround>()
        .End();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (int onGround in _onGrounds)
      {
        SetHorizontalSpeed(onGround, _config.MovementSpeed);
      }

      foreach (int onNotGround in _onNotGrounds)
      {
        SetHorizontalSpeed(onNotGround, _config.JumpHorizontalSpeed);
      }
    }

    private void SetHorizontalSpeed(int entity, float value)
    {
      ref HorizontalSpeed speed = ref _game.Get<HorizontalSpeed>(entity);
      speed.Value = value;
    }
  }
}