using Leopotam.EcsLite;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.BehaviourTrees
{
  public interface INodeStrategy
  {
    public EcsPackedEntity Entity { get; set; }
  }
}