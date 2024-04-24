using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Hook
{
  public class InterruptHookSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _precastCommands;
    private readonly EcsEntities _pullCommands;

    public InterruptHookSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _precastCommands = _game
        .Filter<InterruptHookCommand>()
        .Inc<HookPrecast>()
        .Collect();

      _pullCommands = _game
        .Filter<InterruptHookCommand>()
        .Inc<HookPulling>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity command in _precastCommands)
      {
        command
          .Del<HookPrecast>()
          .EnsureDel<OnHookPrecastStarted>()
          .EnsureDel<OnHookPrecastFinished>()
          .Add<OnHookInterrupted>();
      }

      foreach (EcsEntity command in _pullCommands)
      {
        command
          .Del<InterruptHookCommand>()
          .Add<OnHookInterrupted>()
          .Del<HookPulling>()
          .Del<HookTimer>()
          .EnsureDel<OnHookPullingStarted>()
          .EnsureDel<OnHookPullingFinished>()
          .Replace((ref GravityScale gravity) =>
          {
            gravity.Enabled = true;
            gravity.Override = true;
          })
          .Replace((ref MovementVector vector) =>
          {
            vector.Immutable = false;
            vector.Speed = Vector2.zero;
          });
      }
    }
  }
}