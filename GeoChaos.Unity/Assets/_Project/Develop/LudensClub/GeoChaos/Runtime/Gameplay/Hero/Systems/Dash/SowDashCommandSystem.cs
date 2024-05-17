using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core.Dash
{
  public class SowDashCommandSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsFilter _cooldowns;
    private readonly EcsEntities _dashes;

    public SowDashCommandSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _cooldowns = _game
        .Filter<DashAvailable>()
        .Inc<DashCommand>()
        .Inc<DashCooldown>()
        .End();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (int cooldown in _cooldowns)
        _game.Del<DashCommand>(cooldown);
    }
  }
}