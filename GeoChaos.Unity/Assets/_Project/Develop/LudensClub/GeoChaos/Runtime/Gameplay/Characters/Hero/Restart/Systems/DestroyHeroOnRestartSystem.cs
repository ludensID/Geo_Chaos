using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Core.Destroying;
using LudensClub.GeoChaos.Runtime.Gameplay.Restart;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Restart
{
  public class DestroyHeroOnRestartSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _heroes;
    private readonly EcsWorld _message;
    private readonly EcsEntities _restartMessages;

    public DestroyHeroOnRestartSystem(MessageWorldWrapper messageWorldWrapper, GameWorldWrapper gameWorldWrapper)
    {
      _message = messageWorldWrapper.World;
      _game = gameWorldWrapper.World;

      _restartMessages = _message
        .Filter<BeforeRestartMessage>()
        .Collect();

      _heroes = _game
        .Filter<HeroTag>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity _ in _restartMessages)
      foreach (EcsEntity hero in _heroes)
      {
        hero.Add<DestroyCommand>();
      }
    }
  }
}