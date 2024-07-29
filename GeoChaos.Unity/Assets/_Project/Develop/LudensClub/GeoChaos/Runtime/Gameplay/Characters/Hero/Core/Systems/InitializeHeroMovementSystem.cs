using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Attack;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Bump;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Dash;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Jump;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Move;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Shoot;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Shoot.Aim;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Creation;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Systems
{
  public class InitializeHeroMovementSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly HeroConfig _config;
    private readonly EcsEntities _heroes;

    public InitializeHeroMovementSystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider)
    {
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _heroes = _game
        .Filter<HeroTag>()
        .Inc<OnConverted>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity hero in _heroes)
      {
        hero
          .Change((ref HorizontalSpeed speed) => speed.Speed = _config.MovementSpeed)
          .Change((ref GravityScale gravity) => gravity.Value = _config.GravityScale)
          .Has<DashAvailable>(_config.EnableDash)
          .Has<AttackAvailable>(_config.EnableAttack)
          .Has<HookAvailable>(_config.EnableHook)
          .Has<InterruptHookAvailable>(_config.AllowHookInterruption)
          .Has<DragForceAvailable>(_config.EnableDragForce)
          .Has<ADControllable>(_config.EnableADControl)
          .Has<ShootAvailable>(_config.EnableShoot)
          .Has<AimAvailable>(_config.EnableAim)
          .Has<BumpAvailable>(_config.EnableBump);
      }
    }
  }
}