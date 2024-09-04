using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Glide;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Jump;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Input;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Gravity;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Jump
{
  public class ReadInputForHeroJumpSystem : IEcsRunSystem
  {
    private readonly EcsWorld _world;
    private readonly EcsEntities _landedHeroes;
    private readonly EcsEntities _jumpStartedInputs;
    private readonly EcsEntities _liftedHeroes;
    private readonly EcsEntities _jumpCanceledInputs;

    public ReadInputForHeroJumpSystem(GameWorldWrapper gameWorldWrapper, InputWorldWrapper inputWorldWrapper)
    {
      _world = gameWorldWrapper.World;
      EcsWorld inputWorld = inputWorldWrapper.World;

      _landedHeroes = _world
        .Filter<HeroTag>()
        .Inc<JumpAvailable>()
        .Inc<MovementLayout>()
        .Collect();

      _jumpStartedInputs = inputWorld
        .Filter<Expired>()
        .Inc<IsJumpStarted>()
        .Collect();

      _liftedHeroes = _world
        .Filter<HeroTag>()
        .Inc<JumpAvailable>()
        .Inc<Jumping>()
        .Collect();

      _jumpCanceledInputs = inputWorld
        .Filter<Expired>()
        .Inc<IsJumpCanceled>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity _ in _jumpStartedInputs)
      foreach (EcsEntity hero in _landedHeroes
        .Check<MovementLayout>(x => x.Layer == MovementLayer.All))
      {
        if (hero.Has<OnGround>() 
          || hero.AddOrGet<LastGlideMovement>().Movement is MovementType.Dash or MovementType.Hook)
        {
          hero.Add<JumpCommand>();
        }
      }

      foreach (EcsEntity _ in _jumpCanceledInputs)
      foreach (EcsEntity hero in _liftedHeroes
        .Check<MovementLayout>(x => x.Layer == MovementLayer.All))
      {
        hero.Add<StopJumpCommand>();
      }
    }
  }
}