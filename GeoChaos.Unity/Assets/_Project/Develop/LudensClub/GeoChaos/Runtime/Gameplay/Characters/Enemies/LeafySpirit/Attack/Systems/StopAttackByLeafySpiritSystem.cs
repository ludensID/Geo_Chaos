using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Attack;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Attack
{
  public class StopAttackByLeafySpiritSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _attackingSpirits;

    public StopAttackByLeafySpiritSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _attackingSpirits = _game
        .Filter<LeafySpiritTag>()
        .Inc<StopAttackCommand>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity spirit in _attackingSpirits)
      {
        spirit
          .Del<StopAttackCommand>()
          .Has<Attacking>(false)
          .Has<HitCooldown>(false)
          .Has<OnAttackFinished>(false);
      }
    }
  }
}