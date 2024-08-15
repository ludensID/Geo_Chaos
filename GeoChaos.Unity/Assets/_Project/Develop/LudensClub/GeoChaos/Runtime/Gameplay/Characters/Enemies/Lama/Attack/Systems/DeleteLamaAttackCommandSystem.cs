using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Damage;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Lama.Attack
{
  public class DeleteLamaAttackCommandSystem : DeleteSystem<AttackCommand>
  {
    protected DeleteLamaAttackCommandSystem(GameWorldWrapper wrapper) 
      : base(wrapper, x => x.Inc<LamaTag>())
    {
    }
  }
}