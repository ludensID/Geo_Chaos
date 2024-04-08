using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core.Dash
{
  public class DeleteDashCommandSystem : IEcsRunSystem
  {
    private readonly EcsWorld _world;
    private readonly EcsFilter _commands;

    public DeleteDashCommandSystem(GameWorldWrapper gameWorldWrapper)
    {
      _world = gameWorldWrapper.World;

      _commands = _world.Filter<DashCommand>()
        .End();
    }

    public void Run(EcsSystems systems)
    {
      foreach (var command in _commands) _world.Del<DashCommand>(command);
    }
  }
}