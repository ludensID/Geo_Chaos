using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.AI.BehaviourTrees;

namespace LudensClub.GeoChaos.Runtime.Gameplay.AI
{
  public class ContinueStrategy : IActionStrategy
  {
    public EcsEntity Entity { get; set; }

    public BehaviourStatus Execute()
    {
      return Node.CONTINUE;
    }
  }
}