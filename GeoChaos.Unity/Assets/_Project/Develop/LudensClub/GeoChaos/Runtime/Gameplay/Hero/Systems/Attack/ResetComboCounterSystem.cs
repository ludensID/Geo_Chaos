using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Attack;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Attack
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
        .Where<ComboAttackTimer>(x => x.TimeLeft <= 0))
      {
        timer
          .Replace((ref ComboAttackCounter counter) => counter.Count = 0)
          .Del<ComboAttackTimer>();
      }
    }
  }
}