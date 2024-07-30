using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Attack;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Attack
{
  public class StartAttackByLeafySpiritSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _attackingSpirits;

    public StartAttackByLeafySpiritSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _attackingSpirits = _game
        .Filter<LeafySpiritTag>()
        .Inc<AttackCommand>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity spirit in _attackingSpirits)
      {
        spirit
          .Del<AttackCommand>()
          .Add<Attacking>()
          .Change((ref ComboAttackCounter counter) => counter.Count = 0);
      }
    }
  }
}