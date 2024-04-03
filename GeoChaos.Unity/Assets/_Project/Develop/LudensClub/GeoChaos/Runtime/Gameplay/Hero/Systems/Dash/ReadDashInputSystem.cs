using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core.Dash
{
  public class ReadDashInputSystem : IEcsRunSystem
  {
    private readonly IInputDataProvider _input;
    private readonly EcsWorld _world;
    private readonly EcsFilter _heroes;

    public ReadDashInputSystem(GameWorldWrapper gameWorldWrapper, IInputDataProvider input)
    {
      _input = input;
      _world = gameWorldWrapper.World;

      _heroes = _world.Filter<Hero>()
        .Inc<DashAvailable>()
        .End();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (int hero in _heroes)
      {
        if (!_input.Data.IsDash)
          return;

        _world.Add<DashCommand>(hero);
      }
    }
  }
}