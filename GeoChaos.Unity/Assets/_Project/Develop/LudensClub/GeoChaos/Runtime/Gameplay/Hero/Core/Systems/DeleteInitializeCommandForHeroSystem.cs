using LudensClub.GeoChaos.Runtime.Gameplay.Creation.Components;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class DeleteInitializeCommandForHeroSystem : Delete<InitializeCommand, GameWorldWrapper>
  {
    public DeleteInitializeCommandForHeroSystem(GameWorldWrapper gameWorldWrapper) 
    : base(gameWorldWrapper, x => x.Inc<HeroTag>())
    {
    }
  }
}