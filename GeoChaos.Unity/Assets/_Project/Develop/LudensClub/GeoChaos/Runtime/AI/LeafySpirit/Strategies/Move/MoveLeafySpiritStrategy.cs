using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.BehaviourTrees;

namespace LudensClub.GeoChaos.Runtime.AI
{
  public class MoveLeafySpiritStrategy : IActionStrategy, IResetStrategy
  {
    private readonly EcsWorld _game;
    public EcsPackedEntity Entity { get; set; }

    public MoveLeafySpiritStrategy(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;
    }

    public BehaviourStatus Execute()
    {
      if (Entity.TryUnpackEntity(_game, out EcsEntity spirit))
      {
        if (!spirit.Has<Moving>())
          spirit.Add<MoveCommand>();

        return Node.CONTINUE;
      }

      return Node.FALSE;
    }

    public void Reset()
    {
      if (Entity.TryUnpackEntity(_game, out EcsEntity spirit) 
        && spirit.Has<Moving>())
      {
        spirit.Add<StopMoveCommand>();
      }
    }
  }
}