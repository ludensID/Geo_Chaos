using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Wait;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Wait
{
  public class ZombieWaitFeature : WaitFeature<ZombieTag>
  {
    public ZombieWaitFeature(IEcsSystemFactory systems) : base(systems)
    {
    }
  }
}