using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Bite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Attack.Bite
{
  public class EnableFrogBiteColliderSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _startedEvents;
    private readonly EcsEntities _finishedEvents;
    private readonly EcsEntities _stoppedEvents;

    public EnableFrogBiteColliderSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _startedEvents = _game
        .Filter<FrogTag>()
        .Inc<OnBiteStarted>()
        .Collect();

      _finishedEvents = _game
        .Filter<FrogTag>()
        .Inc<OnBiteFinished>()
        .Collect();

      _stoppedEvents = _game
        .Filter<FrogTag>()
        .Inc<OnBiteStopped>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity frog in _startedEvents)
      {
        EnableCollider(frog, true);
      }

      foreach (EcsEntity frog in _finishedEvents)
      {
        EnableCollider(frog, false);
      }

      foreach (EcsEntity frog in _stoppedEvents)
      {
        EnableCollider(frog, false);
      }
    }

    public void EnableCollider(EcsEntity frog, bool enabled)
    {
      frog.Change((ref BiteColliderRef colliderRef) => colliderRef.Collider.enabled = enabled);
    }
  }
}