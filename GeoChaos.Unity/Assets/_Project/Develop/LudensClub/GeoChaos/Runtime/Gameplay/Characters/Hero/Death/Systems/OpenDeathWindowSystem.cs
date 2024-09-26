using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Die;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Windows;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Death
{
  public class OpenDeathWindowSystem : IEcsRunSystem
  {
    private readonly IWindowManager _windowManager;
    private readonly EcsWorld _game;
    private readonly EcsEntities _diedHeroes;

    public OpenDeathWindowSystem(GameWorldWrapper gameWorldWrapper, IWindowManager windowManager)
    {
      _windowManager = windowManager;
      _game = gameWorldWrapper.World;
      
      _diedHeroes = _game
        .Filter<HeroTag>()
        .Inc<OnDied>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity _ in _diedHeroes)
      {
        _windowManager.Open(WindowType.Death);
      }
    }
  }
}