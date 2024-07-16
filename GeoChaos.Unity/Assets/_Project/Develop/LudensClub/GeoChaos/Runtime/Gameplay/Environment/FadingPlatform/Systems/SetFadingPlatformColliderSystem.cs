using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.View;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.FadingPlatform
{
  public class SetFadingPlatformColliderSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _fadingPlatforms;

    public SetFadingPlatformColliderSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _fadingPlatforms = _game
        .Filter<FadingPlatformTag>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity platform in _fadingPlatforms)
      {
        platform.Get<ColliderRef>().Collider.enabled = !platform.Has<AppearCooldown>();
      }
    }
  }
}