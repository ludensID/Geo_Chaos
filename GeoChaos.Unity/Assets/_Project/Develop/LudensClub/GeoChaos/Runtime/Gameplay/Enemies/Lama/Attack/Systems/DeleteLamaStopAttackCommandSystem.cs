using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemies.Lama.Attack
{
  public class DeleteLamaStopAttackCommandSystem : Delete<StopAttackCommand, GameWorldWrapper>
  {
    protected DeleteLamaStopAttackCommandSystem(GameWorldWrapper wrapper)
      : base(wrapper, x => x.Inc<LamaTag>())
    {
    }
  }
}