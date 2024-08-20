using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Detection;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Detection
{
  public class PrepareFrogToSelectionSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _frogs;

    public PrepareFrogToSelectionSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _frogs = _game
        .Filter<FrogTag>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity frog in _frogs)
      {
        frog.Replace((ref ViewAngle angle) => angle.Angle = 90);
      }
    }
  }
}