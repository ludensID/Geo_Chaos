using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Damage;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Lama.Attack
{
  public class DeleteLamaStopAttackCommandSystem : Delete<StopAttackCommand, GameWorldWrapper>
  {
    protected DeleteLamaStopAttackCommandSystem(GameWorldWrapper wrapper)
      : base(wrapper, x => x.Inc<LamaTag>())
    {
    }
  }
}