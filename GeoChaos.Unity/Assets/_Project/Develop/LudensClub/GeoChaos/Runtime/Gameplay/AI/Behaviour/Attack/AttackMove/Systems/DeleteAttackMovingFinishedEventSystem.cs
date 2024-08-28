using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Attack.AttackMove
{
  public class DeleteAttackMovingFinishedEventSystem<TFilterComponent> : DeleteSystem<OnAttackMovingFinished>
    where TFilterComponent : struct, IEcsComponent
  {
    protected DeleteAttackMovingFinishedEventSystem(GameWorldWrapper gameWorldWrapper)
      : base(gameWorldWrapper, x => x.Inc<TFilterComponent>())
    {
    }
  }
}