using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Damage;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Attack.State
{
  public class DeleteAttackStartedEventSystem<TFilterComponent> : DeleteSystem<OnAttackStarted>
    where TFilterComponent : struct, IEcsComponent
  {
    protected DeleteAttackStartedEventSystem(GameWorldWrapper gameWorldWrapper) 
      : base(gameWorldWrapper, x => x.Inc<TFilterComponent>())
    {
    }
  }
}