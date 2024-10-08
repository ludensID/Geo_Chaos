﻿using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Chase;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Lama.Chase
{
  public class DeleteLamaStopChaseCommandSystem : DeleteSystem<StopChaseCommand>
  {
    protected DeleteLamaStopChaseCommandSystem(GameWorldWrapper wrapper) 
        : base(wrapper, x => x.Inc<LamaTag>())
    {
    }
  }
}