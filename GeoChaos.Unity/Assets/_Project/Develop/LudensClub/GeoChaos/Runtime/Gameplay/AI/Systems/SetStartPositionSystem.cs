using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Creation.Components;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.AI
{
  public class SetStartPositionSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _initializedCommands;

    public SetStartPositionSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _initializedCommands = _game
        .Filter<InitializeCommand>()
        .Inc<Brain>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity command in _initializedCommands)
      {
        var transform = command.Get<ViewRef>().View.transform;
        command.Add((ref StartTransform start) =>
        {
          start.Position = transform.position;
          start.Rotation = transform.rotation;
        });
      }
    }
  }
}