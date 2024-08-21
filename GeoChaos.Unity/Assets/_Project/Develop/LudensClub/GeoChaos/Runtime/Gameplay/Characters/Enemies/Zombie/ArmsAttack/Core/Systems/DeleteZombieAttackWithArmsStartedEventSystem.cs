using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.ArmsAttack
{
  public class DeleteZombieAttackWithArmsStartedEventSystem : DeleteSystem<OnAttackWithArmsStarted>
  {
    protected DeleteZombieAttackWithArmsStartedEventSystem(GameWorldWrapper gameWorldWrapper) 
      : base(gameWorldWrapper, x => x.Inc<ZombieTag>())
    {
    }
  }
}