using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.AI.BehaviourTrees;

namespace LudensClub.GeoChaos.Runtime.Gameplay.AI
{
  public class ActionStrategy<TStateComponent, TStartComponent> : IActionStrategy
    where TStateComponent : struct, IEcsComponent
    where TStartComponent : struct, IEcsComponent
  {
    public EcsEntity Entity { get; set; }
    
    public virtual BehaviourStatus Execute()
    {
      if (!Entity.Has<TStateComponent>())
        Entity.Add<TStartComponent>();

      return Node.CONTINUE;
    }
  }
}