using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Wait;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Wait
{
  public class FinishFrogWaitingSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _waitingFrogs;

    public FinishFrogWaitingSystem(GameWorldWrapper gameWorldWrapper)
    { 
      _game = gameWorldWrapper.World;

      _waitingFrogs = _game
        .Filter<FrogTag>()
        .Inc<WaitingTimer>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity frog in _waitingFrogs
        .Check<WaitingTimer>(x => x.TimeLeft <= 0))
      {
        frog.Del<WaitingTimer>();
      }
    }
  }
}