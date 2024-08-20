using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Patrol;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Patrol
{
  public class DeleteZombiePatrolStartedEventSystem : DeleteSystem<OnPatrolStarted>
  {
    protected DeleteZombiePatrolStartedEventSystem(GameWorldWrapper gameWorldWrapper) 
      : base(gameWorldWrapper, x => x.Inc<ZombieTag>())
    {
    }
  }
}