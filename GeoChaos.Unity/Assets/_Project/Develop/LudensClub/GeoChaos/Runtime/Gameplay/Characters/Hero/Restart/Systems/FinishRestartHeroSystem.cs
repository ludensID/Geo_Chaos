using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Creation;
using LudensClub.GeoChaos.Runtime.Gameplay.Restart;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Restart
{
  public class FinishRestartHeroSystem : IEcsRunSystem
  {
    private readonly EcsWorld _message;
    private readonly EcsWorld _game;
    private readonly EcsEntities _restartMessages;
    private readonly EcsEntities _heroes;

    public FinishRestartHeroSystem(MessageWorldWrapper messageWorldWrapper, GameWorldWrapper gameWorldWrapper)
    {
      _message = messageWorldWrapper.World;
      _game = gameWorldWrapper.World;

      _restartMessages = _message
        .Filter<AfterRestartMessage>()
        .Collect();

      _heroes = _game
        .Filter<HeroTag>()
        .Exc<OnConverted>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity restart in _restartMessages)
      foreach (EcsEntity _ in _heroes)
      {
        restart.Add<OnHeroRestarted>();
      }
    }
  }
}