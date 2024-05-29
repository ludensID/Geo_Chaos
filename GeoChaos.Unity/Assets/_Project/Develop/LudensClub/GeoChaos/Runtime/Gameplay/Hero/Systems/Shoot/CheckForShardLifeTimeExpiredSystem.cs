using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Core.Destroying;
using LudensClub.GeoChaos.Runtime.Gameplay.Shard;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Shoot
{
  public class CheckForShardLifeTimeExpiredSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _expiredShards;

    public CheckForShardLifeTimeExpiredSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;
      
      _expiredShards = _game
        .Filter<LifeTime>()
        .Inc<EntityId>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity shard in _expiredShards
        .Where<EntityId>(x => x.Id == EntityType.Shard)
        .Where<LifeTime>(x => x.TimeLeft <= 0))
      {
        shard.Add<DestroyCommand>();
      }
    }
  }
}