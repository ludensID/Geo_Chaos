﻿using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Interaction
{
  public class DeleteHeroInteractCommandSystem : DeleteSystem<InteractCommand>
  {
    protected DeleteHeroInteractCommandSystem(GameWorldWrapper gameWorldWrapper) 
      : base(gameWorldWrapper, x => x.Inc<HeroTag>())
    {
    }
  }
}