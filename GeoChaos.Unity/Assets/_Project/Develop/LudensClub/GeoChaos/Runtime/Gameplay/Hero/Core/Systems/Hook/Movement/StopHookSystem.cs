using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Attack;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Immunity;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Hook
{
  public class StopHookSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _commands;
    private readonly EcsEntities _selectedRings;
    private readonly HeroConfig _config;

    public StopHookSystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider)
    {
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _commands = _game
        .Filter<StopHookCommand>()
        .Inc<Hooking>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity command in _commands)
      {
        command
          .Del<StopHookCommand>()
          .Del<Hooking>();

        if (_config.BumpOnHookReaction == BumpOnHookReactionType.Immunity && command.Has<Immune>()
          && command.Get<Immune>().Owner == MovementType.Hook)
        {
          command
            .Del<Immune>()
            .Add<OnImmunityFinished>();
        }
        
        ref MovementLayout layout = ref command.Get<MovementLayout>();
        if (layout.Owner == MovementType.Hook)
        {
          layout.Layer = MovementLayer.All;
          layout.Owner = MovementType.None;
        }
      }
    }
  }
}