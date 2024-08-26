using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Shroom.Attack.Reload
{
  public class DeleteShroomReloadingFinishedEventSystem : DeleteSystem<OnReloadingFinished>
  {
    protected DeleteShroomReloadingFinishedEventSystem(GameWorldWrapper gameWorldWrapper)
      : base(gameWorldWrapper, x => x.Inc<ShroomTag>())
    {
    }
  }
}