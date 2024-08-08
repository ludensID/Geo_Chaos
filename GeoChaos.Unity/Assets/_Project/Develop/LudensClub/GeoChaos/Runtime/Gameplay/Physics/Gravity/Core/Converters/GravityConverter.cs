using System;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Gravity
{
  [Serializable]
  public class GravityConverter : IEcsSerializedConverter
  {
    public bool EnableGravityOnStart = true;
    
    public void ConvertTo(EcsEntity entity)
    {
      entity
        .Add((ref GravityScale gravity) => gravity.Enabled = EnableGravityOnStart)
        .Add<Ground>()
        .Add<GroundCheckTimer>();
    }

    public void ConvertBack(EcsEntity entity)
    {
    }
  }
}