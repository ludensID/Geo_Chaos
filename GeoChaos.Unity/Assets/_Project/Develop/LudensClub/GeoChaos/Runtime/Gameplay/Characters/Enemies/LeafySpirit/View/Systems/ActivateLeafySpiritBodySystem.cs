using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Relaxation;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Rise;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.View;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.View
{
  public class ActivateLeafySpiritBodySystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _risingSpirits;
    private readonly EcsEntities _relaxingSpirits;

    public ActivateLeafySpiritBodySystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _risingSpirits = _game
        .Filter<LeafySpiritTag>()
        .Inc<OnRiseFinished>()
        .Collect();

      _relaxingSpirits = _game
        .Filter<LeafySpiritTag>()
        .Inc<OnRelaxationFinished>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity spirit in _risingSpirits)
      {
        ActivateBody(spirit, true);
      }

      foreach (EcsEntity spirit in _relaxingSpirits)
      {
        ActivateBody(spirit, false);
      }
    }

    private static void ActivateBody(EcsEntity spirit, bool active)
    {
      spirit
        .Change((ref ColliderRef colliderRef) => colliderRef.Collider.gameObject.SetActive(active))
        .Change((ref CalmColliderRef colliderRef) => colliderRef.Collider.gameObject.SetActive(!active));
    }
  }
}