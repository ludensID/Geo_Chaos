using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.Door
{
  public class DeleteOnDoorOpenedSystem: Delete<OnOpened>
  {
    protected DeleteOnDoorOpenedSystem(GameWorldWrapper gameWorldWrapper) 
      : base(gameWorldWrapper, x => x.Inc<DoorTag>())
    {
    }
  }
}