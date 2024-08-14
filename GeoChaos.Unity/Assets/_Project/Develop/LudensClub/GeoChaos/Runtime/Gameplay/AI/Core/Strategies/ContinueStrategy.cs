using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Infrastructure.AI.BehaviourTrees;

namespace LudensClub.GeoChaos.Runtime.Gameplay.AI
{
  public class ContinueStrategy : IActionStrategy
  {
    public EcsPackedEntity Entity { get; set; }

    public BehaviourStatus Execute()
    {
      return Node.CONTINUE;
    }
  }
}