using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Damage;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Attack
{
  public class FinishAttackByLeafySpiritSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _attackingSpirits;

    public FinishAttackByLeafySpiritSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _attackingSpirits = _game
        .Filter<LeafySpiritTag>()
        .Inc<FinishAttackCommand>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity spirit in _attackingSpirits)
      {
        spirit
          .Del<FinishAttackCommand>()
          .Del<Attacking>()
          .Add<OnAttackFinished>();
      }
    }
  }
}