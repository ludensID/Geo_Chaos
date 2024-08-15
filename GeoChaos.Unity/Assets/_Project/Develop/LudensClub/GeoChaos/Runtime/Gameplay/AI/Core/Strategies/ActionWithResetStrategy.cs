using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.AI.BehaviourTrees;

namespace LudensClub.GeoChaos.Runtime.Gameplay.AI
{
  public class ActionWithResetStrategy<TStateComponent, TStartComponent, TStopComponent> :
    ActionStrategy<TStateComponent, TStartComponent>, IResetStrategy
    where TStateComponent : struct, IEcsComponent
    where TStartComponent : struct, IEcsComponent
    where TStopComponent : struct, IEcsComponent
  {
    public virtual void Reset()
    {
      if (Entity.Has<TStateComponent>())
        Entity.Add<TStopComponent>();
    }
  }
}