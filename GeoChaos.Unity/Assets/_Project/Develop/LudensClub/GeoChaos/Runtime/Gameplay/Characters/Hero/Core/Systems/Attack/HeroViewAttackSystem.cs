using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Attack;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Attack;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Systems.Attack
{
  public class HeroViewAttackSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _onStartAttacks;
    private readonly EcsEntities _onFinishAttacks;

    public HeroViewAttackSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _onStartAttacks = _game
        .Filter<OnAttackStarted>()
        .Inc<ComboAttackCounter>()
        .Inc<HeroAttackCollidersRef>()
        .Collect();

      _onFinishAttacks = _game
        .Filter<OnAttackFinished>()
        .Inc<ComboAttackCounter>()
        .Inc<HeroAttackCollidersRef>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity attack in _onStartAttacks)
      {
        attack.Change((ref HeroAttackCollidersRef colliders) =>
          colliders.Colliders[attack.Get<ComboAttackCounter>().Count].enabled = true);
      }

      foreach (EcsEntity attack in _onFinishAttacks)
      {
        int index = attack.Get<ComboAttackCounter>().Count - 1;
        if (index < 0)
          index += 3;

        attack.Change((ref HeroAttackCollidersRef colliders) => colliders.Colliders[index].enabled = false);
      }
    }
  }
}