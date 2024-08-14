using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Stun
{
  public class FrogStunFeature : EcsFeature
  {
    public FrogStunFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<StunFrogSystem>());
      Add(systems.Create<FinishStunFrogSystem>());
    }
  }
}