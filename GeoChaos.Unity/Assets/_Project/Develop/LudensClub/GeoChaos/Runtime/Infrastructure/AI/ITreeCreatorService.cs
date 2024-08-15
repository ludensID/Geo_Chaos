using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure.AI.BehaviourTrees;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.AI
{
  public interface ITreeCreatorService
  {
    BehaviourTree Create(EntityType id, EcsEntity entity);
  }
}