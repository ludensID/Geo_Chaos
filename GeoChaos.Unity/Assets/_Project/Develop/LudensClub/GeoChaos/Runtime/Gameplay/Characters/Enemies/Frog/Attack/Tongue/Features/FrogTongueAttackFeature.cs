using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Attack.Tongue
{
  public class FrogTongueAttackFeature : EcsFeature
  {
    public FrogTongueAttackFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<FrogTongueAttackSystem>());
    }
  }
}