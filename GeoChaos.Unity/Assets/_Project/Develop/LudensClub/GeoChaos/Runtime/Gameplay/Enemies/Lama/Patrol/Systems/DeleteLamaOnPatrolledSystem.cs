using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemies.Lama.Patrol
{
  public class DeleteLamaOnPatrolledSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _lamaPatrolledEvents;

    public DeleteLamaOnPatrolledSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _lamaPatrolledEvents = _game
        .Filter<LamaTag>()
        .Inc<OnPatrollFinished>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity patrolled in _lamaPatrolledEvents)
        patrolled.Del<OnPatrollFinished>();
    }
  }
}