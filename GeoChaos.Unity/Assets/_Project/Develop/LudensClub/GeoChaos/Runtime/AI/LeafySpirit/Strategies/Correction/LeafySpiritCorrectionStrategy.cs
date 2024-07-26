using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Enemies.LeafySpirit.Correction;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.BehaviourTrees;

namespace LudensClub.GeoChaos.Runtime.AI
{
  public class LeafySpiritCorrectionStrategy : IActionStrategy
  {
    private readonly EcsWorld _game;
    public EcsPackedEntity Entity { get; set; }

    public LeafySpiritCorrectionStrategy(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;
    }
    
    public BehaviourStatus Execute()
    {
      if (Entity.TryUnpackEntity(_game, out EcsEntity spirit))
      {
        spirit.Add<CorrectCommand>();
        return Node.CONTINUE;
      }

      return Node.FALSE;
    }
  }
}