using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Enemies.Lama;
using LudensClub.GeoChaos.Runtime.Gameplay.Enemies.Lama.Patrol;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.BehaviourTrees;

namespace LudensClub.GeoChaos.Runtime.AI
{
  public class CheckLamaForPatrolStrategy : IConditionStrategy
  {
    private readonly EcsWorld _game;
    public EcsPackedEntity Entity { get; set; }

    public CheckLamaForPatrolStrategy(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;
    }
    
    public bool Check()
    {
      return Entity.TryUnpackEntity(_game, out EcsEntity lama)
        && (!lama.Has<LookingTimer>() || lama.Has<OnPatrollFinished>());
    }
  }
}