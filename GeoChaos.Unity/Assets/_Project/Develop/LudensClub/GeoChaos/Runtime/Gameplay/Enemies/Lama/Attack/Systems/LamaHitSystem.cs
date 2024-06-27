using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Attack;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemies.Lama.Attack
{
  public class LamaHitSystem : IEcsRunSystem
  {
    private readonly ITimerFactory _timers;
    private readonly EcsWorld _game;
    private readonly EcsEntities _lamas;
    private readonly LamaConfig _config;

    public LamaHitSystem(GameWorldWrapper gameWorldWrapper, ITimerFactory timers, IConfigProvider configProvider)
    {
      _timers = timers;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<LamaConfig>();

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
          ref ComboAttackCounter counter = ref lama.Get<ComboAttackCounter>();

          counter.Count++;
          float attackTime = counter.Count == 3 ? _config.BiteTime : _config.HitTime;

          lama
            .Add<OnHitStarted>()
            .Add((ref HitTimer timer) => timer.TimeLeft = _timers.Create(attackTime));
        }
      }
    }
  }
}