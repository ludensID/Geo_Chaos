﻿using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.Checkpoint
{
  public class CheckpointFeature : EcsFeature
  {
    public CheckpointFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<LoadCheckpointSystem>());
      Add(systems.Create<OpenCheckpointWindowSystem>());
      
      Add(systems.Create<DeleteCheckpointOpenedEventSystem>());
      Add(systems.Create<OpenCheckpointSystem>());
    }
  }
}