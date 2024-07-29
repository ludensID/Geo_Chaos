using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Attack;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Lama.Attack
{
  public class StopLamaAttackSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _lamas;

    public StopLamaAttackSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _lamas = _game
        .Filter<LamaTag>()
        .Inc<StopAttackCommand>()
        .Inc<Attacking>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity lama in _lamas)
      {
        lama
          .Del<Attacking>()
          .Change((ref ComboAttackCounter counter) => counter.Count = 0)
          .Has<HitCooldown>(false)
          .Has<ComboCooldown>(false)
          .Has<OnAttackStarted>(false)
          .Has<ComboCooldownUp>(false)
          .Has<HitCooldownUp>(false)
          .Has<BiteCommand>(false)
          .Has<OnHitStarted>(false);

        lama.Has<OnHitFinished>(lama.Has<HitTimer>());
        lama.Has<HitTimer>(false);
      }
    }
  }
}