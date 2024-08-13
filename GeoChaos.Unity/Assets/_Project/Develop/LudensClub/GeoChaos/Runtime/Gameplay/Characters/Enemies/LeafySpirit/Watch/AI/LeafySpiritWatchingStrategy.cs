using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Detection;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Watch;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.AI.BehaviourTrees;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Watch
{
  public class LeafySpiritWatchingStrategy : IActionStrategy
  {
    private readonly EcsWorld _game;
    public EcsPackedEntity Entity { get; set; }

    public LeafySpiritWatchingStrategy(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;
    }
    
    public BehaviourStatus Execute()
    {
      return Entity.TryUnpackEntity(_game, out EcsEntity spirit) && !spirit.Has<TargetInView>() && spirit.Has<WatchingTimer>()
        ? Node.CONTINUE
        : Node.FALSE;
    }
  }
}