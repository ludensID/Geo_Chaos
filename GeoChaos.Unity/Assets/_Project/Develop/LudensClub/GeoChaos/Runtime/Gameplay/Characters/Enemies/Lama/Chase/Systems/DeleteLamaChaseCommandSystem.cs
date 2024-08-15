using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Chase;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Lama.Chase
{
  public class DeleteLamaChaseCommandSystem : DeleteSystem<ChaseCommand>
  {
    protected DeleteLamaChaseCommandSystem(GameWorldWrapper wrapper) 
      : base(wrapper, x => x.Inc<LamaTag>())
    {
    }
  }
}