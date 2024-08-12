using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Bite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Damage;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Attack.Bite
{
  public class CheckForFrogBiteTimerExpiredSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _bitingFrogs;

    public CheckForFrogBiteTimerExpiredSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _bitingFrogs = _game
        .Filter<FrogTag>()
        .Inc<BiteTimer>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity frog in _bitingFrogs
        .Check<BiteTimer>(x => x.TimeLeft <= 0))
      {
        frog
          .Del<BiteTimer>()
          .Del<Biting>()
          .Add<OnBiteFinished>()
          .Add<FinishAttackCommand>();
      }
    }
  }
}