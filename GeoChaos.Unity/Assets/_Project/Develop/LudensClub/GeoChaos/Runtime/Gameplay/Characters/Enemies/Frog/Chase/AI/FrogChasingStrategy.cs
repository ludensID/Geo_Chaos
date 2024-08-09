using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Chase;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.AI.BehaviourTrees;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Chase.AI
{
  public class FrogChasingStrategy : IActionStrategy, IResetStrategy
  {
    private readonly EcsWorld _game;
    public EcsPackedEntity Entity { get; set; }

    public FrogChasingStrategy(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;
    }

    public BehaviourStatus Execute()
    {
      if (Entity.TryUnpackEntity(_game, out EcsEntity frog))
      {
        if (!frog.Has<Chasing>())
          frog.Add<ChaseCommand>();

        return Node.CONTINUE;
      }

      return Node.FALSE;
    }

    public void Reset()
    {
      if (Entity.TryUnpackEntity(_game, out EcsEntity frog)
        && frog.Has<Chasing>())
      {
        frog.Add<StopChaseCommand>();
      }
    }
  }
}