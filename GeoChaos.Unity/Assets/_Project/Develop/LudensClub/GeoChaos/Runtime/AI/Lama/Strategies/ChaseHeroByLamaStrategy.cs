using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Lama.Chase;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.BehaviourTrees;

namespace LudensClub.GeoChaos.Runtime.AI
{
  public class ChaseHeroByLamaStrategy : IActionStrategy, IResetStrategy
  {
    private readonly EcsWorld _game;
    public EcsPackedEntity Entity { get; set; }

    public ChaseHeroByLamaStrategy(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;
    }

    public BehaviourStatus Execute()
    {
      if (Entity.TryUnpackEntity(_game, out EcsEntity lama))
      {
        if (!lama.Has<Chasing>())
          lama.Add<ChaseCommand>();

        return Node.CONTINUE;
      }

      return Node.FALSE;
    }

    public void Reset()
    {
      if (Entity.TryUnpackEntity(_game, out EcsEntity lama))
        lama.Add<StopChaseCommand>();
    }
  }
}