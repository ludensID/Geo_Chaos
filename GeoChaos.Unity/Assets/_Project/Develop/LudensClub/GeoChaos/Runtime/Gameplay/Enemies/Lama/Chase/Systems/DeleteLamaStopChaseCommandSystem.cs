using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemies.Lama.Chase
{
  public class DeleteLamaStopChaseCommandSystem : Delete<StopChaseCommand, GameWorldWrapper>
  {
    protected DeleteLamaStopChaseCommandSystem(GameWorldWrapper wrapper) 
        : base(wrapper, x => x.Inc<LamaTag>())
    {
    }
  }
}