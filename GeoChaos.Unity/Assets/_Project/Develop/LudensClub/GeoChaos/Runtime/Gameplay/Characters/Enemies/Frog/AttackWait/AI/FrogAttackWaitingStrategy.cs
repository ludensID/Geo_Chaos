using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.AI.BehaviourTrees;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.AttackWait
{
  public class FrogAttackWaitingStrategy : IActionStrategy, IResetStrategy
  {
    private readonly EcsWorld _game;
    public EcsPackedEntity Entity { get; set; }

    public FrogAttackWaitingStrategy(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;
    }

    public BehaviourStatus Execute()
    {
      if (Entity.TryUnpackEntity(_game, out EcsEntity frog))
      {
        if (!frog.Has<AttackWaitingTimer>())
          frog.Add<WaitAttackCommand>();

        return Node.CONTINUE;
      }

      return Node.FALSE;
    }

    public void Reset()
    {
      if (Entity.TryUnpackEntity(_game, out EcsEntity frog)
        && frog.Has<AttackWaitingTimer>())
      {
        frog.Add<StopWaitAttackCommand>();
      }
    }
  }
}