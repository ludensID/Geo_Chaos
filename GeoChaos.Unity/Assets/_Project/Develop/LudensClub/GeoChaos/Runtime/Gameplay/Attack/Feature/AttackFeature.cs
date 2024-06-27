using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Attack
{
  public class AttackFeature : EcsFeature
  {
    public AttackFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<GetDamageSystem>());
    }
  }
}