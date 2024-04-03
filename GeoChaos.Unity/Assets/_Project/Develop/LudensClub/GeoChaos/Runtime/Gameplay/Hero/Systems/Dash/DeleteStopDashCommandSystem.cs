using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core.Dash
{
  public class DeleteStopDashCommandSystem : IEcsRunSystem
  {
    private readonly EcsWorld _world;
    private readonly EcsFilter _commands;

    public DeleteStopDashCommandSystem(GameWorldWrapper gameWorldWrapper)
    {
      _world = gameWorldWrapper.World;

      _commands = _world
        .Filter<StopDashCommand>()
        .End();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (int command in _commands)
      {
        _world.Del<StopDashCommand>(command);
      }
    }
  }
}