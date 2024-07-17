using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Interaction
{
  public class InteractSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _heroCommands;
    private readonly EcsEntities _interactions;

    public InteractSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _heroCommands = _game
        .Filter<HeroTag>()
        .Inc<InteractCommand>()
        .Collect();
      
      _interactions = _game
        .Filter<CanInteract>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity _ in _heroCommands)
      foreach (EcsEntity interaction in _interactions)
      {
        interaction.Add<OnInteracted>();
      }
    }
  }
}