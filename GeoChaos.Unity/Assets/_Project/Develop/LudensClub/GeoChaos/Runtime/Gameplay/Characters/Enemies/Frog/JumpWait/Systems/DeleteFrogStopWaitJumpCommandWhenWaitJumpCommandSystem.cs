using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.JumpWait
{
  public class DeleteFrogStopWaitJumpCommandWhenWaitJumpCommandSystem : Delete<StopWaitJumpCommand>
  {
    protected DeleteFrogStopWaitJumpCommandWhenWaitJumpCommandSystem(GameWorldWrapper gameWorldWrapper)
      : base(gameWorldWrapper, x => x.Inc<FrogTag>().Inc<WaitJumpCommand>())
    {
    }
  }
}