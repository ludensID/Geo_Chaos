using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core.Dash;
using LudensClub.GeoChaos.Runtime.Gameplay.Creation.Components;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Attack;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Jump;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Shot;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
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
        .Inc<InitializeCommand>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity hero in _heroes)
      {
        hero
          .Add<ForceAvailable>()
          .Add<Movable>()
          .Add((ref HorizontalSpeed speed) => speed.Value = _config.MovementSpeed)
          .Add((ref MovementVector vector) => vector.Direction.x = 1)
          .Add<LastMovementVector>()
          .Add<ViewDirection>()
          .Add<BodyDirection>()
          .Add<Ground>()
          .Add((ref GroundCheckTimer timer) => timer.TimeLeft = 0)
          .Add<JumpAvailable>()
          .Add((ref GravityScale gravity) =>
          {
            gravity.Enabled = true;
            gravity.Value = _config.GravityScale;
          })
          .Has<DashAvailable>(_config.EnableDash)
          .Has<AttackAvailable>(_config.EnableAttack)
          .Add<ComboAttackCounter>()
          .Has<HookAvailable>(_config.EnableHook)
          .Has<InterruptHookAvailable>(_config.AllowHookInterruption)
          .Has<DragForceAvailable>(_config.EnableDragForce)
          .Has<ADControllable>(_config.EnableADControl)
          .Has<ShootAvailable>(_config.EnableShoot);
      }
    }
  }
}