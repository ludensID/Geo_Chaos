using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure.BehaviourTrees;

namespace LudensClub.GeoChaos.Runtime.AI
{
  public interface IBehaviourTreeCreator
  {
    EntityType Id { get; } 
    BehaviourTree Create(EcsPackedEntity entity);
  }
}