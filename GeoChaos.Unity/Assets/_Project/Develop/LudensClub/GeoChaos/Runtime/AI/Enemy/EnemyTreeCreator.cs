using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure.BehaviourTrees;

namespace LudensClub.GeoChaos.Runtime.AI
{
  public class EnemyTreeCreator : IBehaviourTreeCreator
  {
    public EntityType Id => EntityType.Enemy;

    public BehaviourTree Create(EcsPackedEntity entity)
    {
      return new BehaviourTree(entity);
    }
  }
}