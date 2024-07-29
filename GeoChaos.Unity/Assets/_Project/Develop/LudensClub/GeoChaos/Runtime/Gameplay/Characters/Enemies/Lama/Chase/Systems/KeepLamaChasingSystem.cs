using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Lama.Chase
{
  public class KeepLamaChasingSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _chasingLamas;
    private readonly EcsEntities _heroes;
    private readonly SpeedForceLoop _forceLoop;

    public KeepLamaChasingSystem(GameWorldWrapper gameWorldWrapper, ISpeedForceLoopService forceLoopSvc)
    {
      _game = gameWorldWrapper.World;
      _forceLoop = forceLoopSvc.CreateLoop();

      _chasingLamas = _game
        .Filter<LamaTag>()
        .Inc<Chasing>()
        .Collect();

      _heroes = _game
        .Filter<HeroTag>()
        .Inc<ViewRef>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity hero in _heroes)
      foreach (EcsEntity lama in _chasingLamas)
      {
        Vector3 lamaPosition = lama.Get<ViewRef>().View.transform.position;
        Vector3 heroPosition = hero.Get<ViewRef>().View.transform.position;
        float direction = Mathf.Sign(heroPosition.x - lamaPosition.x);
        
        foreach (EcsEntity force in _forceLoop
          .GetLoop(SpeedForceType.Chase, lama.Pack()))
        {
          force.Change((ref MovementVector vector) => vector.Direction.x = direction);
        }
      }
    }
  }
}