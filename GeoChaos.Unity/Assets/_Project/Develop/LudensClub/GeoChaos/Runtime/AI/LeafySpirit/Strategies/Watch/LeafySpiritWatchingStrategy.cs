using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.BehaviourTrees;

namespace LudensClub.GeoChaos.Runtime.AI
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
      return Entity.TryUnpackEntity(_game, out EcsEntity spirit) && !spirit.Has<Aimed>() && spirit.Has<WatchingTimer>()
        ? Node.CONTINUE
        : Node.FALSE;
    }
  }
}