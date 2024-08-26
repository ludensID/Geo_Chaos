using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Patrol
{
  public class DeletePatrolFinishedEventSystem<TFilterComponent> : DeleteSystem<OnPatrolFinished>
    where TFilterComponent : struct, IEcsComponent
  {
    protected DeletePatrolFinishedEventSystem(GameWorldWrapper gameWorldWrapper) 
      : base(gameWorldWrapper, x => x.Inc<TFilterComponent>())
    {
    }
  }
}