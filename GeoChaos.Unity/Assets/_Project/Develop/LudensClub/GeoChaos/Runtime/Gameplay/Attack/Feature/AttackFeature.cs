using LudensClub.GeoChaos.Runtime.Gameplay.Attack.Systems;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Attack.Feature
{
  public class AttackFeature : EcsFeature
  {
    public AttackFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<GetDamageSystem>());
    }
  }
}