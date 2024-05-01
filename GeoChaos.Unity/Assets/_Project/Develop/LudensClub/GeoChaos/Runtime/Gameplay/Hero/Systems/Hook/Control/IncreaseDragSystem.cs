using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Hook
{
  public class IncreaseDragSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _draggables;

    public IncreaseDragSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _draggables = _game
        .Filter<DragForceAvailable>()
        .Inc<DragForceFactor>()
        .Inc<DragForcing>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity draggable in _draggables)
      {
        draggable.Replace((ref DragForceFactor factor) =>
        {
          factor.Factor += draggable.Get<DragForcing>().Rate * Time.fixedDeltaTime;
          factor.Factor = MathUtils.Clamp(factor.Factor, max: 1);
        });
      }
    }
  }
}