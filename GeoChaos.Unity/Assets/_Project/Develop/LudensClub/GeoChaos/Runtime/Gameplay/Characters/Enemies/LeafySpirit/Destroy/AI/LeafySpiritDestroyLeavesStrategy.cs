using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.AI.BehaviourTrees;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Destroy
{
  public class LeafySpiritDestroyLeavesStrategy : IActionStrategy
  {
    public EcsEntity Entity { get; set; }

    public BehaviourStatus Execute()
    {
        Entity.Add<DestroyLeavesCommand>();
        return Node.TRUE;
    }
  }
}