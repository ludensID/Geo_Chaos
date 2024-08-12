using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Attack.Tongue;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Attack
{
  public class FrogAttackLateFeature : EcsFeature
  {
    public FrogAttackLateFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<FrogTongueAttackLateFeature>());
    }
  }
}