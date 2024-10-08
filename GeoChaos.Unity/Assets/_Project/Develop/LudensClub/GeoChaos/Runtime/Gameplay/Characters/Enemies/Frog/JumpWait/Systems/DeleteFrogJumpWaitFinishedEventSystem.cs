﻿using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.JumpWait
{
  public class DeleteFrogJumpWaitFinishedEventSystem : DeleteSystem<OnJumpWaitFinished>
  {
    protected DeleteFrogJumpWaitFinishedEventSystem(GameWorldWrapper gameWorldWrapper) 
        : base(gameWorldWrapper, x => x.Inc<FrogTag>())
    {
    }
  }
}