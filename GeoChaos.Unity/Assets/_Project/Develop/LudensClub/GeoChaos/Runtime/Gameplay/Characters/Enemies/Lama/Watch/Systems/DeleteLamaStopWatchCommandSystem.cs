﻿using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Lama.Watch
{
  public class DeleteLamaStopWatchCommandSystem : Delete<StopWatchCommand, GameWorldWrapper>
  {
    protected DeleteLamaStopWatchCommandSystem(GameWorldWrapper wrapper) 
        : base(wrapper, x => x.Inc<LamaTag>())
    {
    }
  }
}