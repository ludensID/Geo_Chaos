using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.View;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Rise
{
  public class EnableLeafySpiritBodyColliderSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _risingSpirits;

    public EnableLeafySpiritBodyColliderSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _risingSpirits = _game
        .Filter<LeafySpiritTag>()
        .Inc<OnRiseStarted>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity spirit in _risingSpirits)
      {
        spirit
          .Change((ref ColliderRef colliderRef) => colliderRef.Collider.enabled = true)
          .Change((ref CalmColliderRef colliderRef) => colliderRef.Collider.enabled = false);
      }
    }
  }
}