using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Lock;
using LudensClub.GeoChaos.Runtime.Gameplay.Ring;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Hook
{
  public class StopHookSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _commands;
    private readonly EcsEntities _selectedRings;

    public StopHookSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _commands = _game
        .Filter<OnHookPullingFinished>()
        .Inc<Hooking>()
        .Inc<IsMovementLocked>()
        .Collect();

      _selectedRings = _game
        .Filter<RingTag>()
        .Inc<Hooked>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity command in _commands)
      {
        command
          .Del<Hooking>()
          .Add<UnlockMovementCommand>()
          .Del<HookPulling>()
          .Del<HookTimer>()
          .Replace((ref GravityScale gravity) =>
          {
            gravity.Enabled = true;
            gravity.Override = true;
          });
          
        foreach (EcsEntity ring in _selectedRings)
          ring.Del<Hooked>();
      }
    }
  }
}