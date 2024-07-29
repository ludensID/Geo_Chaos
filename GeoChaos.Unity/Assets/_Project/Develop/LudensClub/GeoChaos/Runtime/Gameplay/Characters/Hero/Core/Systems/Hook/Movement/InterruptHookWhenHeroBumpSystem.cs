using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Systems.Hook
{
  public class InterruptHookWhenHeroBumpSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly HeroConfig _config;
    private readonly EcsEntities _hookedHeroes;

    public InterruptHookWhenHeroBumpSystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider)
    {
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _hookedHeroes = _game
        .Filter<HeroTag>()
        .Inc<Hooking>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity hero in _hookedHeroes)
      {
        ref MovementLayout layout = ref hero.Get<MovementLayout>();
        if (_config.BumpOnHookReaction == BumpOnHookReactionType.Interruption && layout.Owner == MovementType.Bump)
        {
          hero.Has<InterruptHookCommand>(true);
        }
      }
    }
  }
}