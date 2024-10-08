﻿using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Gravity;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Aim
{
  public class SwitchAimSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _startCommands;
    private readonly EcsEntities _finishCommands;

    public SwitchAimSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _startCommands = _game
        .Filter<HeroTag>()
        .Inc<StartAimCommand>()
        .Inc<OnGround>()
        .Collect();

      _finishCommands = _game
        .Filter<HeroTag>()
        .Inc<FinishAimCommand>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity command in _startCommands
        .Check<MovementLayout>(x => (x.Layer & MovementLayer.Stay) > 0))
      {
        command
          .Change((ref MovementLayout layout) =>
          {
            layout.Layer = MovementLayer.Shoot;
            layout.Movement = MovementType.Aim;
          })
          .Add<OnAimStarted>()
          .Add<Aiming>();
      }

      foreach (EcsEntity command in _finishCommands)
      {
        command
          .Add<OnAimFinished>()
          .Del<Aiming>();

        ref MovementLayout layout = ref command.Get<MovementLayout>();
        if (layout.Movement == MovementType.Aim)
        {
          layout.Layer = MovementLayer.All;
          layout.Movement = MovementType.None;
        }
      }
    }
  }
}