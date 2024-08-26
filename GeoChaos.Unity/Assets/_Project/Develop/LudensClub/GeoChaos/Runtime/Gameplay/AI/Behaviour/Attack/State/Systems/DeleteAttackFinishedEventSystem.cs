using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Damage;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Attack.State
{
  public class DeleteAttackFinishedEventSystem<TFilterComponent> : DeleteSystem<OnAttackFinished>
    where TFilterComponent : struct, IEcsComponent
  {
    protected DeleteAttackFinishedEventSystem(GameWorldWrapper gameWorldWrapper) 
      : base(gameWorldWrapper, x => x.Inc<TFilterComponent>())
    {
    }
  }
}