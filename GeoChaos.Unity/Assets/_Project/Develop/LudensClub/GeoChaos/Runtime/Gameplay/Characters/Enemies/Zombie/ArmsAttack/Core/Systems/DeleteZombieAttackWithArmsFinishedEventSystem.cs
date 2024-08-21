using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.ArmsAttack
{
  public class DeleteZombieAttackWithArmsFinishedEventSystem : DeleteSystem<OnAttackWithArmsFinished>
  {
    protected DeleteZombieAttackWithArmsFinishedEventSystem(GameWorldWrapper gameWorldWrapper) 
      : base(gameWorldWrapper, x => x.Inc<ZombieTag>())
    {
    }
  }
}