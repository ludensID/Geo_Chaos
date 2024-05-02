using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Move
{
  public class IncreaseControlSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _controllings;

    public IncreaseControlSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _controllings = _game
        .Filter<Controllable>()
        .Inc<Controlling>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity controlling in _controllings)
      {
        controlling.Replace((ref ControlFactor factor) =>
        {
          factor.Factor += controlling.Get<Controlling>().Rate * Time.fixedDeltaTime;
          factor.Factor = MathUtils.Clamp(factor.Factor, max: 1);
        });
      }
    }
  }
}