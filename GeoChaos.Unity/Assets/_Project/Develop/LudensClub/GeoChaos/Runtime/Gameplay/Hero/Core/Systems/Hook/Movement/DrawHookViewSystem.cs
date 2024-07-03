using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Ring;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Hook
{
  public class DrawHookViewSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _heroes;
    private readonly EcsEntities _rings;
    private readonly EcsEntities _finishedHooks;
    private readonly EcsEntities _interruptedHooks;

    public DrawHookViewSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _heroes = _game
        .Filter<HeroTag>()
        .Inc<HookPulling>()
        .Inc<HookRef>()
        .Inc<ViewRef>()
        .Collect();

      _rings = _game
        .Filter<RingTag>()
        .Inc<Hooked>()
        .Collect();

      _finishedHooks = _game
        .Filter<OnHookPullingFinished>()
        .Inc<HookRef>()
        .Collect();

      _interruptedHooks = _game
        .Filter<OnHookInterrupted>()
        .Inc<HookRef>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity ring in _rings)
      foreach (EcsEntity hero in _heroes)
      {
        ref HookRef hookRef = ref hero.Get<HookRef>();
        hookRef.Hook.positionCount = 2;
        hookRef.Hook.SetPositions(new[]
        {
          hero.Get<ViewRef>().View.transform.position,
          ring.Get<ViewRef>().View.transform.position
        });
      }

      foreach (EcsEntity hook in _finishedHooks)
      {
        hook.Get<HookRef>().Hook.positionCount = 0;
      }

      foreach (var hook in _interruptedHooks)
      {
        hook.Get<HookRef>().Hook.positionCount = 0;
      }
    }
  }
}