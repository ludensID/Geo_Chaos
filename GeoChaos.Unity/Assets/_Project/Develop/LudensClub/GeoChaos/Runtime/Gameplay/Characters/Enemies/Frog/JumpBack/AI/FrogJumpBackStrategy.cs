using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.AI.BehaviourTrees;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.JumpBack
{
  public class FrogJumpBackStrategy : IActionStrategy, IResetStrategy
  {
    private readonly EcsWorld _game;
    public EcsPackedEntity Entity { get; set; }

    public FrogJumpBackStrategy(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;
    }
      
    public BehaviourStatus Execute()
    {
      if (Entity.TryUnpackEntity(_game, out EcsEntity frog))
      {
        if (!frog.Has<JumpingBack>())
          frog.Add<JumpBackCommand>();

        return Node.CONTINUE;
      }

      return Node.FALSE;
    }

    public void Reset()
    {
      if (Entity.TryUnpackEntity(_game, out EcsEntity frog)
        && frog.Has<JumpingBack>())
      {
        frog.Add<StopJumpBackCommand>();
      }
    }
  }
}