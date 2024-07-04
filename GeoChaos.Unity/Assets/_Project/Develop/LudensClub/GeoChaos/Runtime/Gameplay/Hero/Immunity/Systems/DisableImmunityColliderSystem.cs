using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Immunity
{
  public class DisableImmunityColliderSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _castedColliderEvents;

    public DisableImmunityColliderSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;
        
      _castedColliderEvents = _game
        .Filter<HeroTag>()
        .Inc<OnImmunityColliderCasted>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity cast in _castedColliderEvents)
      {
        cast
          .Change((ref ImmunityColliderRef collider) => collider.Collider.enabled = false)
          .Del<OnImmunityColliderCasted>();
      }
    }
  }
}