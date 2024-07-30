using LudensClub.GeoChaos.Runtime.Gameplay.Attack;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Systems.Attack
{
  public class DeleteHeroAttackStartedEventSystem : Delete<OnAttackStarted>
  {
    protected DeleteHeroAttackStartedEventSystem(GameWorldWrapper gameWorldWrapper)
      : base(gameWorldWrapper, x => x.Inc<HeroTag>())
    {
    }
  }
}