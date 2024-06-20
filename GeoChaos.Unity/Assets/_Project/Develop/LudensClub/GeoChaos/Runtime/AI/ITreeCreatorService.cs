using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure.BehaviourTrees;

namespace LudensClub.GeoChaos.Runtime.AI
{
  public interface ITreeCreatorService
  {
    BehaviourTree Create(EntityType id, EcsPackedEntity entity);
  }
}