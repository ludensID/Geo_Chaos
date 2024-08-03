using Leopotam.EcsLite;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.AI.BehaviourTrees
{
  public interface INodeStrategy
  {
    public EcsPackedEntity Entity { get; set; }
  }
}