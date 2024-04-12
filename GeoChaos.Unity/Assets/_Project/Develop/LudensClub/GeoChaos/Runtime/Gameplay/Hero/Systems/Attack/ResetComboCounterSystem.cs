using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Attack;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Attack
{
  public class ResetComboCounterSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsFilter _timers;

    public ResetComboCounterSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _timers = _game
        .Filter<ComboAttackTimer>()
        .Inc<ComboAttackCounter>()
        .End();
    }

    public void Run(EcsSystems systems)
    {
      foreach (int timer in _timers
        .Where((ref ComboAttackTimer x) => x.TimeLeft <= 0))
      {
        ref ComboAttackCounter counter = ref _game.Get<ComboAttackCounter>(timer);
        counter.Count = 0;
        
        _game.Del<ComboAttackTimer>(timer);
      }
    }
  }
}