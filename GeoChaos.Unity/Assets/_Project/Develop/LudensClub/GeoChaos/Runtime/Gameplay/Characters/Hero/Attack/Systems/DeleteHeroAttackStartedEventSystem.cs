using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Damage;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Attack
{
  public class DeleteHeroAttackStartedEventSystem : Delete<OnAttackStarted>
  {
    protected DeleteHeroAttackStartedEventSystem(GameWorldWrapper gameWorldWrapper)
      : base(gameWorldWrapper, x => x.Inc<HeroTag>())
    {
    }
  }
}