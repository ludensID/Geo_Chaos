using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Attack;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog
{
  public class FrogLateFeature : EcsFeature
  {
    public FrogLateFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<FrogAttackLateFeature>());
    }
  }
}