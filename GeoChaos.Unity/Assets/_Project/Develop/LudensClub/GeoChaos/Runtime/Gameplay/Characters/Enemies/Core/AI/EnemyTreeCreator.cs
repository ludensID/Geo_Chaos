using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.AI;
using LudensClub.GeoChaos.Runtime.Infrastructure.AI.BehaviourTrees;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies
{
  public class EnemyTreeCreator : IBehaviourTreeCreator
  {
    public EntityType Id => EntityType.Enemy;

    public BehaviourTree Create(EcsEntity entity)
    {
      return new BehaviourTree(entity);
    }
  }
}