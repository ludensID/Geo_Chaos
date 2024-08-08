using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Creation;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Gravity;
using LudensClub.GeoChaos.Runtime.Gameplay.View;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog
{
  public class InitializeFrogSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _convertedFrogs;

    public InitializeFrogSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _convertedFrogs = _game
        .Filter<FrogTag>()
        .Inc<OnConverted>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity frog in _convertedFrogs)
      {
        frog.Change((ref GravityScale gravity) => gravity.Scale = frog.Get<RigidbodyRef>().Rigidbody.gravityScale);
      }
    }
  }
}