using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Hook
{
  public class CheckForHeroReachRingSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _pullings;

    public CheckForHeroReachRingSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _pullings = _game
        .Filter<HookPulling>()
        .Inc<ViewRef>()
        .Inc<MovementVector>()
        .Exc<StopHookPullingCommand>()
        .Collect();
    } 
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity pulling in _pullings)
      {
        Transform heroTransform = pulling.Get<ViewRef>().View.transform;
        ref HookPulling hookPulling = ref pulling.Get<HookPulling>();
        if ((hookPulling.Target.x - heroTransform.position.x) * Mathf.Sign(hookPulling.Velocity.x) <= 0)
        {
          pulling.Add<StopHookPullingCommand>();
          pulling.Replace((ref MovementVector vector) =>
          {
            vector.Immutable = false;
          });
        }
      }
    }
  }
}