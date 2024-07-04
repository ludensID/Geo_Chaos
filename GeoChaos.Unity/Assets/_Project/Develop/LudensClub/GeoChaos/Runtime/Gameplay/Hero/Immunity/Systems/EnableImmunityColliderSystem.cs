using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.View;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Immunity
{
  public class EnableImmunityColliderSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _finishedImmunityEvents;

    public EnableImmunityColliderSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _finishedImmunityEvents = _game
        .Filter<HeroTag>()
        .Inc<OnImmunityFinished>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity immunity in _finishedImmunityEvents)
      {
        if (immunity.Get<ColliderRef>().Collider.enabled)
        {
          immunity
            .Change((ref ImmunityColliderRef collider) => collider.Collider.enabled = true)
            .Add<OnImmunityColliderCasted>();
        }

        immunity.Del<OnImmunityFinished>();
      }
    }
  }
}