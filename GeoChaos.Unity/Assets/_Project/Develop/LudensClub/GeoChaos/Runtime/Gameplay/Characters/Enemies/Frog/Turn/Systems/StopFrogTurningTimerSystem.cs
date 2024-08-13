using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Detection;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Turn
{
  public class StopFrogTurningTimerSystem : Delete<TurningTimer>
  {
    protected StopFrogTurningTimerSystem(GameWorldWrapper gameWorldWrapper)
      : base(gameWorldWrapper, x => x.Inc<FrogTag>().Exc<TargetInBack>())
    {
    }
  }
}