using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.ArmsAttack
{
  public class CheckForZombieAttackWithArmsTimerExpiredSystem : IEcsRunSystem
  {
    private readonly ITimerFactory _timers;
    private readonly EcsWorld _game;
    private readonly EcsEntities _attackingZombies;
    private readonly ZombieConfig _config;

    public CheckForZombieAttackWithArmsTimerExpiredSystem(GameWorldWrapper gameWorldWrapper,
      IConfigProvider configProvider,
      ITimerFactory timers)
    {
      _timers = timers;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<ZombieConfig>();

      _attackingZombies = _game
        .Filter<ZombieTag>()
        .Inc<AttackWithArmsTimer>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity zombie in _attackingZombies
        .Check<AttackWithArmsTimer>(x => x.TimeLeft <= 0))
      {
        zombie
          .Del<AttackWithArmsTimer>()
          .Add<OnAttackWithArmsFinished>()
          .Add((ref AttackWithArmsCooldown cooldown) => cooldown.TimeLeft = _timers.Create(_config.ArmsCooldown));
      }
    }
  }
}