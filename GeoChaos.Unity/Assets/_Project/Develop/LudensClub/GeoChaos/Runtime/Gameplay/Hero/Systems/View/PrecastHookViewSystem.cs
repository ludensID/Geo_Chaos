using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.View
{
  public class PrecastHookViewSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _precastedHooks;
    private readonly EcsEntities _startedPrecasts;
    private readonly EcsEntities _finishedPrecasts;

    public PrecastHookViewSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _startedPrecasts = _game
        .Filter<OnHookPrecastStarted>()
        .Inc<ViewRef>()
        .Inc<HookRef>()
        .Collect();

      _precastedHooks = _game
        .Filter<HookPrecast>()
        .Inc<HookRef>()
        .Collect();

      _finishedPrecasts = _game
        .Filter<OnHookPrecastFinished>()
        .Inc<ViewRef>()
        .Inc<HookRef>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity precast in _startedPrecasts)
      {
        ref HookRef hookRef = ref precast.Get<HookRef>();
        ref ViewRef viewRef = ref precast.Get<ViewRef>();
        var points = new Vector3[2];
        points[0] = viewRef.View.transform.position;
        points[1] = points[0];

        hookRef.Hook.positionCount = 2;
        hookRef.Hook.SetPositions(points);
      }

      foreach (EcsEntity hook in _precastedHooks)
      {
        ref HookRef hookRef = ref hook.Get<HookRef>();
        ref HookPrecast precast = ref hook.Get<HookPrecast>();
        
        var points = new Vector3[2];
        hookRef.Hook.GetPositions(points);
        
        Vector2 deltaVector = Vector2.ClampMagnitude(precast.Velocity * Time.deltaTime,
          Vector2.Distance(precast.TargetPoint, points[1]));
        points[1] += (Vector3)deltaVector;

        hookRef.Hook.SetPositions(points);
      }
    }
  }
}