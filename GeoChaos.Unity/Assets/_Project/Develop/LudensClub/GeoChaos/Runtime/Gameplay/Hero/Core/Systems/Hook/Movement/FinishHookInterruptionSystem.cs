using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Hook;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Hook
{
  public class FinishHookInterruptionSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _interruptedHooks;

    public FinishHookInterruptionSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;
      
      _interruptedHooks = _game
        .Filter<OnHookInterrupted>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity hook in _interruptedHooks)
      {
        hook
          .Del<Hooking>()
          .Del<StopHookCommand>();
        
        ref MovementLayout layout = ref hook.Get<MovementLayout>();
        if (layout.Owner == MovementType.Hook)
        {
          layout.Layer = MovementLayer.All;
          layout.Owner = MovementType.None;
        }
      }
    }
  }
}