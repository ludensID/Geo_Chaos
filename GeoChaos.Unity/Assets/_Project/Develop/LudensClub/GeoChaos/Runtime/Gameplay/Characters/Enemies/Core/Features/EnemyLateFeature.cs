using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies
{
  public class EnemyLateFeature : EcsFeature
  {
    public EnemyLateFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<FrogLateFeature>());
    }
  }
}