using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Dash;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Input;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Jump
{
  public class ReadJumpDelayedInputSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsWorld _input;
    private readonly EcsEntities _heroes;
    private readonly EcsEntities _inputs;

    public ReadJumpDelayedInputSystem(GameWorldWrapper gameWorldWrapper, InputWorldWrapper inputWorldWrapper)
    {
      _game = gameWorldWrapper.World;
      _input = inputWorldWrapper.World;

      _heroes = _game
        .Filter<HeroTag>()
        .Inc<JumpAvailable>()
        .Inc<MovementLayout>()
        .Collect();

      _inputs = _input
        .Filter<Expired>()
        .Inc<IsJumpStarted>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity _ in _inputs)
      foreach (EcsEntity hero in _heroes
        .Check<MovementLayout>(x => x.Layer == MovementLayer.Interrupt))
      {
        hero
          .Add<DelayJumpCommand>()
          .Add<InterruptHookCommand>();
      }
    }
  }
}