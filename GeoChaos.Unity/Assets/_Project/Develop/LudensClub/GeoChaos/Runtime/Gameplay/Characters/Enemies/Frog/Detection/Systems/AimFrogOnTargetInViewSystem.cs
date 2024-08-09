using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Detection;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Selection;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Detection
{
  public class AimFrogOnTargetInViewSystem : IEcsRunSystem
  {
    private readonly FrogTargetInViewSelector _selector;
    private readonly EcsWorld _game;
    private readonly EcsEntities _heroes;
    private readonly EcsEntities _frogs;
    private readonly EcsEntities _markedFrogs;

    public AimFrogOnTargetInViewSystem(GameWorldWrapper gameWorldWrapper, FrogTargetInViewSelector selector)
    {
      _selector = selector;
      _game = gameWorldWrapper.World;

      _heroes = _game
        .Filter<HeroTag>()
        .Collect();

      _frogs = _game
        .Filter<FrogTag>()
        .Collect();

      _markedFrogs = _game
        .Filter<FrogTag>()
        .Inc<Marked>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      _selector.Select<TargetInView>(_heroes, _frogs, _markedFrogs);
    }
  }
}