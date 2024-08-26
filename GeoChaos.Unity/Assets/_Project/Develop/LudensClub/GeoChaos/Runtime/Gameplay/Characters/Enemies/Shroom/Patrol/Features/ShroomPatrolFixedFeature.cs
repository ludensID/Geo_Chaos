using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Patrol;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Shroom.Patrol
{
  public class ShroomPatrolFixedFeature : PatrolToRandomPointFixedFeature<ShroomTag>
  {
    public ShroomPatrolFixedFeature(IEcsSystemFactory systems) : base(systems)
    {
    }
  }
}