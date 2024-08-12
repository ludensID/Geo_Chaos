using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Damage;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Attack.Tongue
{
  public class FinishFrogTongueAttackSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _frogs;

    public FinishFrogTongueAttackSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _frogs = _game
        .Filter<FrogTag>()
        .Inc<ThrownTongue>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity frog in _frogs)
      {
        if (!frog.Get<ThrownTongue>().Tongue.TryUnpackEntity(_game, out _))
        {
          frog
            .Del<ThrownTongue>()
            .Add<FinishAttackCommand>();
        }
      }
    }
  }
}