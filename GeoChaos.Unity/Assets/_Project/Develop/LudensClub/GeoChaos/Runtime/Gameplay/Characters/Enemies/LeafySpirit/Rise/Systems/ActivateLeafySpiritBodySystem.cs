using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.View;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Rise
{
  public class ActivateLeafySpiritBodySystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _risingSpirits;

    public ActivateLeafySpiritBodySystem(GameWorldWrapper gameWorldWrapper)
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
          .Change((ref ColliderRef colliderRef) => colliderRef.Collider.gameObject.SetActive(true))
          .Change((ref CalmColliderRef colliderRef) => colliderRef.Collider.gameObject.SetActive(false));
      }
    }
  }
}