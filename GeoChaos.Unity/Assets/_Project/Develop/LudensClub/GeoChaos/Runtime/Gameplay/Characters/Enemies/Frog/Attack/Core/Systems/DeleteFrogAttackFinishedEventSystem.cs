using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Damage;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Attack
{
  public class DeleteFrogAttackFinishedEventSystem : Delete<OnAttackFinished>
  {
    protected DeleteFrogAttackFinishedEventSystem(GameWorldWrapper gameWorldWrapper)
      : base(gameWorldWrapper, x => x.Inc<FrogTag>())
    {
    }
  }
}