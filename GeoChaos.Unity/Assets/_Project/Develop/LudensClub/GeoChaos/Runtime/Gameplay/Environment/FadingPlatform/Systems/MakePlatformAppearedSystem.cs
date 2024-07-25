using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.FadingPlatform
{
  public class MakePlatformAppearedSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _appearCooldowns;

    public MakePlatformAppearedSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _appearCooldowns = _game
        .Filter<AppearCooldown>()
        .Inc<FadingPlatformTag>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity cooldown in _appearCooldowns
        .Check<AppearCooldown>(x => x.TimeLeft <= 0))
      {
        cooldown.Del<AppearCooldown>();
      }
    }
  }
}