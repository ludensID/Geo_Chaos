using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Attack.Preparing
{
  public class DeleteZombieAttackPreparingFinishedEventSystem : DeleteSystem<OnAttackPreparingFinished>
  {
    protected DeleteZombieAttackPreparingFinishedEventSystem(GameWorldWrapper gameWorldWrapper) 
        : base(gameWorldWrapper, x => x.Inc<ZombieTag>())
    {
    }
  }
}