using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Damage;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Systems.Hook
{
  public class HookSystem : IEcsRunSystem
  {
    private readonly ITimerFactory _timers;
    private readonly EcsWorld _game;
    private readonly EcsEntities _commands;
    private readonly HeroConfig _config;

    public HookSystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider, ITimerFactory timers)
    {
      _timers = timers;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _commands = _game
        .Filter<HookCommand>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity command in _commands)
      {
        command
          .Add<Hooking>()
          .Change((ref MovementLayout layout) =>
          {
            layout.Layer = command.Has<InterruptHookAvailable>() ? MovementLayer.Interrupt : MovementLayer.None;
            layout.Owner = MovementType.Hook;
          })
          .Add((ref HookInputCooldown cooldown) => cooldown.TimeLeft = _timers.Create(_config.HookInputCooldown));

        if (_config.BumpOnHookReaction == BumpOnHookReactionType.Immunity)
        {
          command.Replace((ref Immune immune) => immune.Owner = MovementType.Hook);
        }
      }
    }
  }
}