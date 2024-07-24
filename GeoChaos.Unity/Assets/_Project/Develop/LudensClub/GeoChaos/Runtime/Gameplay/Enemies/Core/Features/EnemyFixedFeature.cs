using LudensClub.GeoChaos.Runtime.Gameplay.Enemies.Lama;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemies
{
  public class EnemyFixedFeature : EcsFeature
  {
    public EnemyFixedFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<LamaFixedFeature>());
    }
  }
}