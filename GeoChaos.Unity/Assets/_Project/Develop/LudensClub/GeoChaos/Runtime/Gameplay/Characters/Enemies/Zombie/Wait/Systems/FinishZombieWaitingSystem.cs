using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Wait;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Wait
{
  public class FinishZombieWaitingSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _waitingZombies;

    public FinishZombieWaitingSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _waitingZombies = _game
        .Filter<ZombieTag>()
        .Inc<WaitingTimer>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity zombie in _waitingZombies
        .Check<WaitingTimer>(x => x.TimeLeft <= 0))
      {
        zombie.Del<WaitingTimer>();
      }
    }
  }
}