using LudensClub.GeoChaos.Runtime.Gameplay.Attack;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Lama.Attack
{
  public class DeleteLamaAttackCommandSystem : Delete<AttackCommand, GameWorldWrapper>
  {
    protected DeleteLamaAttackCommandSystem(GameWorldWrapper wrapper) 
      : base(wrapper, x => x.Inc<LamaTag>())
    {
    }
  }
}