using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Stun;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Die;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.Tongue
{
  public class StunFrogWhenTongueDiedSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _diedTongues;

    public StunFrogWhenTongueDiedSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _diedTongues = _game
        .Filter<TongueTag>()
        .Inc<OnDied>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity tongue in _diedTongues)
      {
        if (tongue.Get<Owner>().Entity.TryUnpackEntity(_game, out EcsEntity frog))
        {
          frog.Add<StunCommand>();
        }
      }
    }
  }
}