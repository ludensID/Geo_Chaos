using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Attack;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Lama.Attack
{
  public class FinishLamaComboAttackSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _lamas;

    public FinishLamaComboAttackSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _lamas = _game
        .Filter<LamaTag>()
        .Inc<Attacking>()
        .Inc<ComboCooldownUp>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity lama in _lamas)
      {
        lama
          .Del<Attacking>()
          .Change((ref ComboAttackCounter counter) => counter.Count = 0);
      }
    }
  }
}