using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemies.Lama
{
  public class LamaFeature : EcsFeature
  {
    public LamaFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<InitializeLamaSystem>());
      Add(systems.Create<AddBoundsRefSystem>());
      
      Add(systems.Create<CalculateLamaBoundsSystem>());
      
      Add(systems.Create<PatrolLamaSystem>());
      Add(systems.Create<DeleteLamaPatrolCommandSystem>());
    }
  }
}