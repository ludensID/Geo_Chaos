using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Damage;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Attack
{
  public class DeleteZombieAttackFinishedEventSystem : DeleteSystem<OnAttackFinished>
  {
    protected DeleteZombieAttackFinishedEventSystem(GameWorldWrapper gameWorldWrapper) 
      : base(gameWorldWrapper, x => x.Inc<ZombieTag>())
    {
    }
  }
}