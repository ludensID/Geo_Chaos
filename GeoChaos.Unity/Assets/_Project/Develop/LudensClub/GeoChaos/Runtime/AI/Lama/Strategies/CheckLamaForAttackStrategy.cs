using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Attack;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Lama.Attack;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.BehaviourTrees;

namespace LudensClub.GeoChaos.Runtime.AI
{
  public class CheckLamaForAttackStrategy : IConditionStrategy
  {
    private readonly EcsWorld _game;
    public EcsPackedEntity Entity { get; set; }

    public CheckLamaForAttackStrategy(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;
    }
    
    public bool Check()
    {
      return Entity.TryUnpackEntity(_game, out EcsEntity lama) && (lama.Has<ReadyAttack>() || lama.Has<Attacking>());
    }
  }
}