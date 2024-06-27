using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.AI;
using LudensClub.GeoChaos.Runtime.Gameplay.AI;
using LudensClub.GeoChaos.Runtime.Gameplay.Attack;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemies.Lama.Attack
{
  public class LamaHitSystem : IEcsRunSystem
  {
    private readonly ITimerFactory _timers;
    private readonly EcsWorld _game;
    private readonly EcsEntities _lamas;

    public LamaHitSystem(GameWorldWrapper gameWorldWrapper, ITimerFactory timers)
    {
      _timers = timers;
      _game = gameWorldWrapper.World;

      _lamas = _game
        .Filter<LamaTag>()
        .Inc<Attacking>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity lama in _lamas)
      {
        if(lama.Has<HitCooldownUp>() || lama.Has<BiteCommand>() || lama.Has<OnAttackStarted>())
        {
          var ctx = lama.Get<BrainContext>().Cast<LamaContext>();
          ref ComboAttackCounter counter = ref lama.Get<ComboAttackCounter>();

          counter.Count++;
          float attackTime = counter.Count == 3 ? ctx.BiteTime : ctx.HitTime;

          lama
            .Add<OnHitStarted>()
            .Add((ref HitTimer timer) => timer.TimeLeft = _timers.Create(attackTime));
        }
      }
    }
  }
}