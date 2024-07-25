using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core.Dash
{
  public class RemoveDashCooldownSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _cooldowns;

    public RemoveDashCooldownSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _cooldowns = _game
        .Filter<DashCooldown>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity cooldown in _cooldowns
        .Check<DashCooldown>(x => x.TimeLeft <= 0))
      {
        cooldown.Del<DashCooldown>();
      }
    }
  }
}