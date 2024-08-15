using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Detection;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Watch;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.AI.BehaviourTrees;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Watch
{
  public class LeafySpiritWatchingStrategy : IActionStrategy
  {
    public EcsEntity Entity { get; set; }

    public BehaviourStatus Execute()
    {
      return !Entity.Has<TargetInView>() && Entity.Has<WatchingTimer>()
        ? Node.CONTINUE
        : Node.FALSE;
    }
  }
}