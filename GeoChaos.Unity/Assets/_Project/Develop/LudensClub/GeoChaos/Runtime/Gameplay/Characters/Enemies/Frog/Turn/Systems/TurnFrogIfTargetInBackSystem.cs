using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Detection;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Turn
{
  public class TurnFrogIfTargetInBackSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _frogs;

    public TurnFrogIfTargetInBackSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _frogs = _game
        .Filter<FrogTag>()
        .Inc<TargetInBack>()
        .Exc<TurnCommand>()
        .Exc<TurningTimer>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity frog in _frogs)
      {
        frog.Add<TurnCommand>();
      }
    }
  }
}