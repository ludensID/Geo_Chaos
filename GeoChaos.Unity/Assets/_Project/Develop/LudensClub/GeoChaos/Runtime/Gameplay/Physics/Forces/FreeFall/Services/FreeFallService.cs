using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Move;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces
{
  public class FreeFallService : IFreeFallService
  {
    private readonly ITimerFactory _timers;

    public FreeFallService(ITimerFactory timers)
    {
      _timers = timers;
    }
    
    public void StopFreeFall(EcsEntity owner, EcsEntities freeFalls)
    {
      foreach (EcsEntity freeFall in freeFalls
        .Where<Owner>(x => x.Entity.EqualsTo(owner.Pack())))
      {
        freeFall
          .Has<Enabled>(false)
          .Has<Prepared>(false)
          .Has<Delay>(false);
      }

      owner.Has<FreeRotating>(false);
      owner.Has<FreeFalling>(false);

      owner.Replace((ref MovementVector vector) => vector.Speed = Vector2.zero);
    }

    public void PrepareFreeFall(EcsEntity freeFall, float time, float coefficient, bool useGradient)
    {
      float gradientTime = time * 2 * (1 - coefficient * 2);
      gradientTime = MathUtils.Clamp(gradientTime, 0.0001f);

      if (useGradient)
      {
        freeFall.Add((ref Delay delay) =>
          delay.TimeLeft = _timers.Create(time * 2 * coefficient));
      }

      freeFall
        .Del<PrepareCommand>()
        .Add<Prepared>()
        .Replace((ref GradientRate rate) => rate.Rate = 1 / gradientTime);
    }
  }
}