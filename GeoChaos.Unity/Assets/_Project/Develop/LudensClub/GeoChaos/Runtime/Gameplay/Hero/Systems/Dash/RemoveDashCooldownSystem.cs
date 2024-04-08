using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core.Dash
{
  public class RemoveDashCooldownSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsFilter _cooldowns;

    public RemoveDashCooldownSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _cooldowns = _game
        .Filter<DashCooldown>()
        .End();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (int cooldown in _cooldowns)
      {
        ref DashCooldown dashCooldown = ref _game.Get<DashCooldown>(cooldown);
        if (dashCooldown.Timer <= 0)
          _game.Del<DashCooldown>(cooldown);
      }
    }
  }
}