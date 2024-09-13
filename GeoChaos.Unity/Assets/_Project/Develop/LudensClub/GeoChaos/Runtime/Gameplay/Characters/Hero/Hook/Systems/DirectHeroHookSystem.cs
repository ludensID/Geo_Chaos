using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Hook
{
  public class DirectHeroHookSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _pullingHeroes;
    private readonly SpeedForceLoop _forceLoop;

    public DirectHeroHookSystem(GameWorldWrapper gameWorldWrapper, ISpeedForceLoopService forceLoopSvc)
    {
      _game = gameWorldWrapper.World;
      _forceLoop = forceLoopSvc.CreateLoop();

      _pullingHeroes = _game
        .Filter<HeroTag>()
        .Inc<OnHookPullingStarted>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity hero in _pullingHeroes)
      foreach (EcsEntity force in _forceLoop
        .GetLoop(SpeedForceType.Hook, hero.PackedEntity))
      {
        Vector2 position = hero.Get<ViewRef>().View.transform.position;
        Vector2 target = hero.Get<HookPulling>().Target;
        ref MovementVector vector = ref force.Get<MovementVector>();
        
        Vector2 velocity = vector.Speed * vector.Direction;
        velocity = (target - position).normalized * velocity.magnitude;
        (Vector2 length, Vector2 direction) = MathUtils.DecomposeVector(velocity);
        vector.Speed = length;
        vector.Direction = direction;
      }
    }
  }
}