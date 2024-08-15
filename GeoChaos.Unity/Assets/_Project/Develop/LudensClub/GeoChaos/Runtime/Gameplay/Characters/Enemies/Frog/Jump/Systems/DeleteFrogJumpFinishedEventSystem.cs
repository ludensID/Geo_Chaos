using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Jump;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Jump
{
  public class DeleteFrogJumpFinishedEventSystem : DeleteSystem<OnJumpFinished>
  {
    protected DeleteFrogJumpFinishedEventSystem(GameWorldWrapper gameWorldWrapper) 
        : base(gameWorldWrapper, x => x.Inc<FrogTag>())
    {
    }
  }
}