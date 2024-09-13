using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.Checkpoint
{
  public class DeleteCheckpointOpenedEventSystem : DeleteSystem<OnOpened>
  {
    protected DeleteCheckpointOpenedEventSystem(GameWorldWrapper gameWorldWrapper) 
        : base(gameWorldWrapper, x => x.Inc<CheckpointTag>())
    {
    }
  }
}