using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Hook
{
  public class LandHeroAfterHookSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _finishes;

    public LandHeroAfterHookSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _finishes = _game
        .Filter<OnHookPullingFinished>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity finish in _finishes)
      {
        finish.Add<HookLanding>();
      }
    }
  }
}