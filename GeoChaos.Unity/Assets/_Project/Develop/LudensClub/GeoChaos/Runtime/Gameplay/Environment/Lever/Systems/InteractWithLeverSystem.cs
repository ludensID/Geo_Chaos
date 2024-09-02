using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Environment.Door;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.Lever
{
  public class InteractWithLeverSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _interactedLevers;

    public InteractWithLeverSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _interactedLevers = _game
        .Filter<LeverTag>()
        .Inc<OnInteracted>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity lever in _interactedLevers)
      {
        DoorView doorView = lever.Get<MatchedDoorRef>().Door;
        if (doorView && doorView.Entity.TryUnpackEntity(_game, out EcsEntity door))
        {
          door.Add<OpenCommand>();
          lever
            .Del<CanInteract>()
            .Del<Interactable>()
            .Del<OnInteracted>(); 
        }
      }
    }
  }
}