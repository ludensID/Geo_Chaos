using LudensClub.GeoChaos.Runtime.Infrastructure;
using NSubstitute;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemy
{
  public class EnemyFeature : EcsFeature
  {
    public EnemyFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<DeleteInitializeCommandForEnemySystem>());
      Add(systems.Create<CreateEnemySystem>());
      Add(systems.Create<SetHealthViewSystem>());
    }
  }
}