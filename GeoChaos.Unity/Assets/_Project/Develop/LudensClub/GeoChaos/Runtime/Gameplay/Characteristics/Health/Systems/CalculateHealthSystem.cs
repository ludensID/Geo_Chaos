using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characteristics.Health
{
  public class CalculateHealthSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _calculateCommands;

    public CalculateHealthSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _calculateCommands = _game
        .Filter<CalculateHealthCommand>()
        .Inc<MaxCurrentHealth>()
        .Inc<DefaultHealth>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity command in _calculateCommands)
      {
        command
          .Del<CalculateHealthCommand>()
          .Add<OnHealthCalculated>()
          .Change((ref MaxCurrentHealth maxHealth) => maxHealth.Health = command.Get<DefaultHealth>().Health);
      }
    }
  }
}