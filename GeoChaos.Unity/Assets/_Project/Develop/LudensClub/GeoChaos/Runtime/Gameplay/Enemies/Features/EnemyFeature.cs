using LudensClub.GeoChaos.Runtime.Gameplay.Enemies.Lama;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemies
{
  public class EnemyFeature : EcsFeature
  {
    public EnemyFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<DeleteInitializeCommandForEnemySystem>());
      Add(systems.Create<CreateEnemySystem>());
      
      Add(systems.Create<LamaFeature>());
      
      Add(systems.Create<SetEnemyBodyDirectionSystem>());
      Add(systems.Create<SetHealthViewSystem>());
    }
  }
}