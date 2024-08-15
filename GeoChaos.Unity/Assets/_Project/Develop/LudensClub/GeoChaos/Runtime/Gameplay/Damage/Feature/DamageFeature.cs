using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Damage
{
  public class DamageFeature : EcsFeature
  {
    public DamageFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<DeleteSystem<OnDamaged, MessageWorldWrapper>>());
      Add(systems.Create<DealDamageSystem>());
      
      Add(systems.Create<ConvertDamageMessageToDealDamageMessageSystem>());
    }
  }
}