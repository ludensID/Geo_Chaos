using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Attack;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Attack;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Systems.Attack
{
  public class ResetComboCounterSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _timers;

    public ResetComboCounterSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _timers = _game
        .Filter<ComboAttackTimer>()
        .Inc<ComboAttackCounter>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity timer in _timers
        .Check<ComboAttackTimer>(x => x.TimeLeft <= 0))
      {
        timer
          .Change((ref ComboAttackCounter counter) => counter.Count = 0)
          .Del<ComboAttackTimer>();
      }
    }
  }
}