using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core.Dash
{
  public class CheckForHeroDashSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsFilter _dashes;
    private readonly EcsFilter _cooldowns;

    public CheckForHeroDashSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _dashes = _game
        .Filter<DashAvailable>()
        .Inc<DashCommand>()
        .Inc<Dashing>()
        .End();

      _cooldowns = _game
        .Filter<DashAvailable>()
        .Inc<DashCommand>()
        .Inc<DashCooldown>()
        .End();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (int hero in _dashes)
        _game.Del<DashCommand>(hero);

      foreach (int cooldown in _cooldowns)
        _game.Del<DashCommand>(cooldown);
    }
  }
}