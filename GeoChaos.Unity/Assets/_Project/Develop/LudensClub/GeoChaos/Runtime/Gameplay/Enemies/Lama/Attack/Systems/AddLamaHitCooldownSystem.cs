using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.AI;
using LudensClub.GeoChaos.Runtime.Gameplay.AI;
using LudensClub.GeoChaos.Runtime.Gameplay.Attack;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemies.Lama.Attack
{
  public class AddLamaHitCooldownSystem : IEcsRunSystem
  {
    private readonly ITimerFactory _timers;
    private readonly EcsWorld _game;
    private readonly EcsEntities _lamas;

    public AddLamaHitCooldownSystem(GameWorldWrapper gameWorldWrapper, ITimerFactory timers)
    {
      _timers = timers;
      _game = gameWorldWrapper.World;

      _lamas = _game
        .Filter<LamaTag>()
        .Inc<Attacking>()
        .Inc<OnHitFinished>()
        .Exc<BiteCommand>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity lama in _lamas)
      {
        var ctx = lama.Get<BrainContext>().Cast<LamaContext>();
        int count = lama.Get<ComboAttackCounter>().Count;
        
        bool endCombo = count >= 2;
        float cooldown = endCombo ? ctx.ComboCooldown : ctx.HitCooldown;
        Timer timer = _timers.Create(cooldown);
        if (endCombo)
          lama.Add((ref ComboCooldown comboCooldown) => comboCooldown.TimeLeft = timer);
        else
          lama.Add((ref HitCooldown hitCooldown) => hitCooldown.TimeLeft = timer);
      }
    }
  }
}