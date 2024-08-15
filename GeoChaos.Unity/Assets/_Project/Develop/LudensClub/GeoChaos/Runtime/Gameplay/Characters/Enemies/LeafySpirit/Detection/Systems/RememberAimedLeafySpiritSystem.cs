using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Detection;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Detection
{
  public class RememberAimedLeafySpiritSystem : AddSystem<WasAimed, LeafySpiritTag>
  {
    public RememberAimedLeafySpiritSystem(GameWorldWrapper gameWorldWrapper) 
        : base(gameWorldWrapper, x => x.Inc<Aimed>())
    {
    }
  }
}