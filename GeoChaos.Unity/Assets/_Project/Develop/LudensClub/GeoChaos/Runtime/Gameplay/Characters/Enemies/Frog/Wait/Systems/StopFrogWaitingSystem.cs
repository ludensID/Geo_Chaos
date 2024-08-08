using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Wait;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Wait
{
  public class StopFrogWaitingSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _waitingFrogs;

    public StopFrogWaitingSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _waitingFrogs = _game
        .Filter<FrogTag>()
        .Inc<StopWaitCommand>()
        .Collect();
    } 
   
    public void Run(EcsSystems systems)
    {
      foreach (var frog in _waitingFrogs)
      {
        frog
          .Del<StopWaitCommand>()
          .Has<WaitingTimer>(false);
      }
    }
  }
}