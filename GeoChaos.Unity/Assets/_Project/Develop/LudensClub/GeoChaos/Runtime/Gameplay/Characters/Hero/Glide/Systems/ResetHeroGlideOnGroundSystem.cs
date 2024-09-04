using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Gravity;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Glide
{
  public class ResetHeroGlideOnGroundSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _glidingHeroes;

    public ResetHeroGlideOnGroundSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _glidingHeroes = _game
        .Filter<HeroTag>()
        .Inc<OnGround>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity hero in _glidingHeroes)
      {
        hero.Replace((ref LastGlideMovement glide) => glide.Movement = MovementType.None);
      }
    }
  }
}