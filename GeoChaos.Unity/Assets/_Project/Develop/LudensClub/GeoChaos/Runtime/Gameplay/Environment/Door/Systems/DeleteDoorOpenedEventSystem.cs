using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.Door
{
  public class DeleteDoorOpenedEventSystem: DeleteSystem<OnOpened>
  {
    protected DeleteDoorOpenedEventSystem(GameWorldWrapper gameWorldWrapper) 
      : base(gameWorldWrapper, x => x.Inc<DoorTag>())
    {
    }
  }
}