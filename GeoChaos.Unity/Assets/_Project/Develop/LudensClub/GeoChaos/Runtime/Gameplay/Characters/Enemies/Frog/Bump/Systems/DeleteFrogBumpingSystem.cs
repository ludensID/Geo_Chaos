using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Bump;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Gravity.Tracking;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Bump
{
  public class DeleteFrogBumpingSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _bumpingFrogs;

    protected DeleteFrogBumpingSystem(GameWorldWrapper gameWorldWrapper) 
    {
      _game = gameWorldWrapper.World;

      _bumpingFrogs = _game
        .Filter<FrogTag>()
        .Inc<Bumping>()
        .Inc<OnLandingDetected>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity frog in _bumpingFrogs)
      {
        frog
          .Del<Bumping>()
          .Del<OnLandingDetected>();
      }      
    }
  }
}