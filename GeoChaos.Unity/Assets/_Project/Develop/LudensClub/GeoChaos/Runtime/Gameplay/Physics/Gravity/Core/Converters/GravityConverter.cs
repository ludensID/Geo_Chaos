﻿using System;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Gravity
{
  [Serializable]
  public class GravityConverter : IEcsSerializedConverter
  {
    public bool EnableGravityOnStart = true;
    public float GravityScale = 1;
    
    public void ConvertTo(EcsEntity entity)
    {
      entity
        .Add((ref GravityScale gravity) =>
        {
          gravity.Enabled.Value = EnableGravityOnStart;
          gravity.Scale.Value = GravityScale;
        })
        .Add<Ground>()
        .Add((ref GroundCheckTimer timer) => timer.TimeLeft = 0);
    }

    public void ConvertBack(EcsEntity entity)
    {
    }
  }
}