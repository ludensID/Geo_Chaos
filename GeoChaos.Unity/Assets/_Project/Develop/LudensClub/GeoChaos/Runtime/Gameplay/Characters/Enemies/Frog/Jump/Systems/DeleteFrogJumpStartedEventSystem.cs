using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Jump;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Jump
{
  public class DeleteFrogJumpStartedEventSystem : Delete<OnJumpStarted>
  {
    protected DeleteFrogJumpStartedEventSystem(GameWorldWrapper gameWorldWrapper) 
        : base(gameWorldWrapper, x => x.Inc<FrogTag>())
    {
    }
  }
}