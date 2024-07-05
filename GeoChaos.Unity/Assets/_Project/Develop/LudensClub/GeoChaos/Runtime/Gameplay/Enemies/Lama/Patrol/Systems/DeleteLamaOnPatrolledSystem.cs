using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemies.Lama.Patrol
{
  public class DeleteLamaOnPatrolledSystem : Delete<OnPatrollFinished, GameWorldWrapper>
  {
    public DeleteLamaOnPatrolledSystem(GameWorldWrapper gameWorldWrapper)
    : base(gameWorldWrapper, x => x.Inc<LamaTag>())
    {
    }
  }
}