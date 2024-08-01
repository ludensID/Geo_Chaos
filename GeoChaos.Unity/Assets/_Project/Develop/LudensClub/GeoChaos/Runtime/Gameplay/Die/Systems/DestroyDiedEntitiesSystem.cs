using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Core.Destroying;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Die
{
  public class DestroyDiedEntitiesSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _diedEntities;

    public DestroyDiedEntitiesSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _diedEntities = _game
        .Filter<OnDied>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity entity in _diedEntities)
      {
        entity.Add<DestroyCommand>();
      }
    }
  }
}