using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Attack;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Lama.Attack;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Lama.View
{
  public class EnableLamaAttackColliderSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _lamas;

    public EnableLamaAttackColliderSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _lamas = _game
        .Filter<LamaTag>()
        .Inc<OnHitStarted>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity lama in _lamas)
      {
        ref LamaAttackCollidersRef colliders = ref lama.Get<LamaAttackCollidersRef>();
        bool isCombo = lama.Get<ComboAttackCounter>().Count == 3;
        if (isCombo)
          colliders.ComboCollider.enabled = true;
        else
          colliders.HitCollider.enabled = true;
      }
    }
  }
}