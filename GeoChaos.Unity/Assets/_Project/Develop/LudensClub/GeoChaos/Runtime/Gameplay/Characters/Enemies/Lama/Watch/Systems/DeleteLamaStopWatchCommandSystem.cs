using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Watch;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Lama.Watch
{
  public class DeleteLamaStopWatchCommandSystem : DeleteSystem<StopWatchCommand>
  {
    protected DeleteLamaStopWatchCommandSystem(GameWorldWrapper wrapper) 
        : base(wrapper, x => x.Inc<LamaTag>())
    {
    }
  }
}