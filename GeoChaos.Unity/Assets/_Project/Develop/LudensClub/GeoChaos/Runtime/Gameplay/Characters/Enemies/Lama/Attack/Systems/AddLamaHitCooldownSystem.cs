using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Attack;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Lama.Attack
{
  public class AddLamaHitCooldownSystem : IEcsRunSystem
  {
    private readonly ITimerFactory _timers;
    private readonly EcsWorld _game;
    private readonly EcsEntities _lamas;
    private readonly LamaConfig _config;

    public AddLamaHitCooldownSystem(GameWorldWrapper gameWorldWrapper,
      ITimerFactory timers,
      IConfigProvider configProvider)
    {
      _timers = timers;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<LamaConfig>();

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
        int count = lama.Get<ComboAttackCounter>().Count;
        
        bool endCombo = count >= 2;
        float cooldown = endCombo ? _config.ComboCooldown : _config.HitCooldown;
        Timer timer = _timers.Create(cooldown);
        if (endCombo)
          lama.Add((ref ComboCooldown comboCooldown) => comboCooldown.TimeLeft = timer);
        else
          lama.Add((ref HitCooldown hitCooldown) => hitCooldown.TimeLeft = timer);
      }
    }
  }
}