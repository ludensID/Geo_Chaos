using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.View;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies
{
  public class SetEnemyBodyDirectionSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _enemies;

    public SetEnemyBodyDirectionSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _enemies = _game
        .Filter<EnemyTag>()
        .Inc<BodyDirection>()
        .Inc<MovementVector>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity enemy in _enemies)
      {
        ref MovementVector vector = ref enemy.Get<MovementVector>();
        ref BodyDirection direction = ref enemy.Get<BodyDirection>();
        if (vector.Speed.x * vector.Direction.x != 0)
          direction.Direction = vector.Direction.x;
      }
    }
  }
}