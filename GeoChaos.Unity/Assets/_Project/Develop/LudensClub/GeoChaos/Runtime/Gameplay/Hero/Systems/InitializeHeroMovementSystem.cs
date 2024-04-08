using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core.Dash;
using LudensClub.GeoChaos.Runtime.Gameplay.Creation.Components;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class InitializeHeroMovementSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsFilter _heroes;
    private readonly HeroConfig _config;

    public InitializeHeroMovementSystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider)
    {
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _heroes = _game
        .Filter<Hero>()
        .Inc<InitializeCommand>()
        .End();
    }

    public void Run(EcsSystems systems)
    {
      foreach (var hero in _heroes)
      {
        ref var movable = ref _game.Add<Movable>(hero);
        movable.CanMove = true;

        ref var vector = ref _game.Add<HeroMovementVector>(hero);
        vector.Direction.x = 1;

        _game.Add<HeroVelocity>(hero);
        _game.Add<Ground>(hero);
        _game.Add<JumpAvailable>(hero);
        ref var gravityScale = ref _game.Add<GravityScale>(hero);
        gravityScale.Value = _config.GravityScale;
        gravityScale.Override = true;

        ref var dash = ref _game.Add<DashAvailable>(hero);
        dash.CanDash = true;
      }
    }
  }
}