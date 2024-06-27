using LudensClub.GeoChaos.Runtime.Gameplay.Enemies.Lama.Attack;
using LudensClub.GeoChaos.Runtime.Gameplay.Enemies.Lama.View;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemies.Lama
{
  public class LamaFeature : EcsFeature
  {
    public LamaFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<InitializeLamaSystem>());
      Add(systems.Create<AddBoundsRefSystem>());
      
      Add(systems.Create<AimOnHeroSystem>());
      Add(systems.Create<CheckHeroInLamaViewSystem>());
      
      Add(systems.Create<DeleteLamaOnPatrolledSystem>());
      Add(systems.Create<PatrolLamaSystem>());
      Add(systems.Create<DeleteLamaPatrolCommandSystem>());
      Add(systems.Create<StopPatrollingSystem>());
      
      Add(systems.Create<ChaseHeroByLamaSystem>());
      Add(systems.Create<KeepLamaChasingSystem>());
      Add(systems.Create<StopChaseHeroByLamaSystem>());
      
      Add(systems.Create<LamaSneakingSystem>());
      Add(systems.Create<StopLamaSneakingSystem>());
      
      Add(systems.Create<LamaAttackFeature>());
      
      Add(systems.Create<LamaViewFeature>());
    }
  }
}