using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure.AI.BehaviourTrees;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.AI
{
  public interface IBehaviourTreeCreator
  {
    EntityType Id { get; } 
    BehaviourTree Create(EcsPackedEntity entity);
  }
}