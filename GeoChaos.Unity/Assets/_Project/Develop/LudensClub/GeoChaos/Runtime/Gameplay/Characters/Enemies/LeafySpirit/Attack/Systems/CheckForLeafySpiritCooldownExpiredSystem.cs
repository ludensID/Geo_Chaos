using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Damage;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Attack
{
  public class CheckForLeafySpiritCooldownExpiredSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _attackingSpirits;

    public CheckForLeafySpiritCooldownExpiredSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _attackingSpirits = _game
        .Filter<LeafySpiritTag>()
        .Inc<HitCooldown>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity spirit in _attackingSpirits
        .Check<HitCooldown>(x => x.TimeLeft <= 0))
      {
        spirit.Del<HitCooldown>();
      }
    }
  }
}