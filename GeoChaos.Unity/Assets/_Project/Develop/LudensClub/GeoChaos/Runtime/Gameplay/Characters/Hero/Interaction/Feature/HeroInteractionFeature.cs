using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Interaction;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Interaction
{
  public class HeroInteractionFeature : EcsFeature
  {
    public HeroInteractionFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<DeleteSystem<OnInteracted>>());
      Add(systems.Create<ReadInteractionInputSystem>());
      Add(systems.Create<InteractSystem>());
      Add(systems.Create<DeleteHeroInteractCommandSystem>());
    }
  }
}