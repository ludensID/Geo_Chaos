using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Attack
{
  public class DeleteFrogAttackStoppedEventSystem : Delete<OnAttackStopped>
  {
    protected DeleteFrogAttackStoppedEventSystem(GameWorldWrapper gameWorldWrapper)
      : base(gameWorldWrapper, x => x.Inc<FrogTag>())
    {
    }
  }
}