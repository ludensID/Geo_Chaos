using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Attack
{
  public class AttackFeature : EcsFeature
  {
    public AttackFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<Delete<OnDamaged, MessageWorldWrapper>>());
      Add(systems.Create<DealDamageSystem>());
    }
  }
}