using LudensClub.GeoChaos.Runtime.Gameplay.Enemies.Lama.Attack;
using LudensClub.GeoChaos.Runtime.Gameplay.Enemies.Lama.Chase;
using LudensClub.GeoChaos.Runtime.Gameplay.Enemies.Lama.Detection;
using LudensClub.GeoChaos.Runtime.Gameplay.Enemies.Lama.Patrol;
using LudensClub.GeoChaos.Runtime.Gameplay.Enemies.Lama.View;
using LudensClub.GeoChaos.Runtime.Gameplay.Enemies.Lama.Watch;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemies.Lama
{
  public class LamaFeature : EcsFeature
  {
    public LamaFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<AddBoundsRefSystem>());
      
      Add(systems.Create<LamaDetectionFeature>());
      Add(systems.Create<LamaPatrolFeature>());
      Add(systems.Create<LamaChasingFeature>());
      Add(systems.Create<LamaWatchingFeature>());
      Add(systems.Create<LamaAttackFeature>());
      Add(systems.Create<LamaViewFeature>());
    }
  }
}