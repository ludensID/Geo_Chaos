using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Destroy;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.BehaviourTrees;

namespace LudensClub.GeoChaos.Runtime.AI
{
  public class LeafySpiritDestroyLeavesStrategy : IActionStrategy
  {
    private readonly EcsWorld _game;
    public EcsPackedEntity Entity { get; set; }

    public LeafySpiritDestroyLeavesStrategy(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;
    }
    
    public BehaviourStatus Execute()
    {
      if (Entity.TryUnpackEntity(_game, out EcsEntity spirit))
      {
        spirit.Add<DestroyLeavesCommand>();
        return Node.TRUE;
      }

      return Node.FALSE;
    }
  }
}