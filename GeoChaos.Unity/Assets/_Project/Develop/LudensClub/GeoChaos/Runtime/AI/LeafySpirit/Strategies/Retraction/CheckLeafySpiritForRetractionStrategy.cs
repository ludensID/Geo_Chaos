using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.BehaviourTrees;

namespace LudensClub.GeoChaos.Runtime.AI
{
  public class CheckLeafySpiritForRetractionStrategy : IConditionStrategy
  {
    private readonly EcsWorld _game;
    public EcsPackedEntity Entity { get; set; }

    public CheckLeafySpiritForRetractionStrategy(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;
    }
    
    public bool Check()
    {
      return Entity.TryUnpackEntity(_game, out EcsEntity spirit) 
        && spirit.Has<Aimed>() && spirit.Has<Discharged>();
    }
  }
}