using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Core.Dash;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Dash;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Input;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Dash
{
  public class ReadDashDelayedInputSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsWorld _input;
    private readonly EcsEntities _heroes;
    private readonly EcsEntities _inputs;

    public ReadDashDelayedInputSystem(GameWorldWrapper gameWorldWrapper, InputWorldWrapper inputWorldWrapper)
    { 
      _game = gameWorldWrapper.World;
      _input = inputWorldWrapper.World;
      
      _heroes = _game
        .Filter<DashAvailable>()
        .Inc<Hooking>()
        .Inc<InterruptHookAvailable>()
        .Exc<DashCooldown>()
        .Collect();

      _inputs = _input
        .Filter<Expired>()
        .Inc<IsDash>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity _ in _inputs)
      foreach (EcsEntity hero in _heroes)
      {
        hero
          .Add<DelayDashCommand>()
          .Add<InterruptHookCommand>();
      }
    }
  }
}