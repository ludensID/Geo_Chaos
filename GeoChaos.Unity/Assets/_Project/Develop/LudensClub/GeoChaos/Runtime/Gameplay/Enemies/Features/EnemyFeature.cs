using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemies
{
  public class EnemyFeature : EcsFeature
  {
    public EnemyFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<DeleteInitializeCommandForEnemySystem>());
      Add(systems.Create<CreateEnemySystem>());
      
      Add(systems.Create<CreateBehaviourTreeSystem>());

      Add(systems.Create<SetHealthViewSystem>());
    }
  }
}