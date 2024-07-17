using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Interaction
{
  public class DeleteHeroInteractCommandSystem : Delete<InteractCommand>
  {
    protected DeleteHeroInteractCommandSystem(GameWorldWrapper gameWorldWrapper) 
      : base(gameWorldWrapper, x => x.Inc<HeroTag>())
    {
    }
  }
}