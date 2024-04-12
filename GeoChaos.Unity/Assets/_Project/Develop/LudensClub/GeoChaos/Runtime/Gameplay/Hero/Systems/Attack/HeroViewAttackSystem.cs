using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Attack;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Attack
{
  public class HeroViewAttackSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsFilter _onStartAttacks;
    private readonly EcsFilter _onFinishAttacks;

    public HeroViewAttackSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _onStartAttacks = _game
        .Filter<OnAttackStarted>()
        .Inc<ComboAttackCounter>()
        .Inc<HeroAttackColliders>()
        .End();

      _onFinishAttacks = _game
        .Filter<OnAttackFinished>()
        .Inc<ComboAttackCounter>()
        .Inc<HeroAttackColliders>()
        .End();
    }

    public void Run(EcsSystems systems)
    {
      foreach (int attack in _onStartAttacks)
      {
        ref ComboAttackCounter counter = ref _game.Get<ComboAttackCounter>(attack);
        ref HeroAttackColliders colliders = ref _game.Get<HeroAttackColliders>(attack);
        colliders.Colliders[counter.Count].enabled = true;
      }

      foreach (int attack in _onFinishAttacks)
      {
        ref ComboAttackCounter counter = ref _game.Get<ComboAttackCounter>(attack);
        ref HeroAttackColliders colliders = ref _game.Get<HeroAttackColliders>(attack);
        int index = counter.Count - 1;
        if (index < 0)
          index += 3;
        colliders.Colliders[index].enabled = false;
      }
    }
  }
}