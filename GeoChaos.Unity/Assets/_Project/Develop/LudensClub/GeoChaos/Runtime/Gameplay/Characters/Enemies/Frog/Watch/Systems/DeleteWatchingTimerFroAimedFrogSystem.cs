using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Watch;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Watch
{
  public class DeleteWatchingTimerFroAimedFrogSystem : Delete<WatchingTimer>
  {
    protected DeleteWatchingTimerFroAimedFrogSystem(GameWorldWrapper gameWorldWrapper) 
      : base(gameWorldWrapper, x => x.Inc<FrogTag>().Inc<Aimed>())
    {
    }
  }
}