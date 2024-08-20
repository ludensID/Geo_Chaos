using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Patrol
{
  public class ZombiePatrolFixedFeature : EcsFeature
  {
    public ZombiePatrolFixedFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<CheckForZombieReachedMovePointSystem>());      
    }    
  }
}