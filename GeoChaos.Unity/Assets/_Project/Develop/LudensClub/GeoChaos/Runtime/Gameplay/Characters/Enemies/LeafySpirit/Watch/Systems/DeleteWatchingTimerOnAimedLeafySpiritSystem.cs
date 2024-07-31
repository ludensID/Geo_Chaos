using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Watch
{
  public class DeleteWatchingTimerOnAimedLeafySpiritSystem : Delete<WatchingTimer>
  {
    protected DeleteWatchingTimerOnAimedLeafySpiritSystem(GameWorldWrapper gameWorldWrapper)
      : base(gameWorldWrapper, x => x.Inc<LeafySpiritTag>().Inc<Aimed>())
    {
    }
  }
}