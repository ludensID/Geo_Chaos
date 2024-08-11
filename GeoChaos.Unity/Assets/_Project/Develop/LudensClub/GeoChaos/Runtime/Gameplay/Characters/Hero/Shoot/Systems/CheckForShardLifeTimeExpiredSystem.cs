using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Core.Destroying;
using LudensClub.GeoChaos.Runtime.Gameplay.Shard;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Shoot
{
  public class CheckForShardLifeTimeExpiredSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _expiredShards;

    public CheckForShardLifeTimeExpiredSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;
      
      _expiredShards = _game
        .Filter<ShardTag>()
        .Inc<EntityId>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity shard in _expiredShards
        .Check<LifeTime>(x => x.TimeLeft <= 0))
      {
        shard.Add<DestroyCommand>();
      }
    }
  }
}